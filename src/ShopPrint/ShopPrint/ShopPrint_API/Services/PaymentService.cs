using System.Security.Cryptography;
using AutoMapper;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Enums;
using ShopPrint_API.Entities.Models;

namespace ShopPrint_API.Services;
public class PaymentService
{
    public readonly IMongoCollection<Payment> _paymentCollection;
    public readonly IMongoCollection<Checkout> _checkoutCollection;
    private readonly IMapper _mapper;
    public readonly UserService _userService;
    public PaymentService(IOptions<MongoSettings> mongoSettingsPar, IMapper mapper, UserService userService)
    {
        MongoService mongoSettings = new MongoService(mongoSettingsPar);
        _paymentCollection = mongoSettings._iMongoDatabase.GetCollection<Payment>("Payment");
        _checkoutCollection = mongoSettings._iMongoDatabase.GetCollection<Checkout>("Checkout");
        _mapper = mapper;
        _userService = userService;
    }

    public async Task<object> Pay(string checkoutId, PixDTO? pix, BankSlipDTO? bankSlip)
    {
        try
        {
            Checkout checkout = await _checkoutCollection.Find(c => c.Id == checkoutId).FirstOrDefaultAsync();
            if (checkout == null)
            {
                throw new Exception("O checkout não foi localizada.");
            }
            if(checkout.finished == true)
            {
                throw new Exception("O checkout já foi finalizado.");
            }
            var exist = _paymentCollection.Find(c => c.checkoutId == checkoutId).FirstOrDefault();
            if(exist != null)
            {
                throw new Exception("Já existe uma ordem de pagamento aberta para esse checkout.");
            }
            switch(checkout.PaymentMethod)
            {
                case PaymentMethod.Pix:
                    Pix entityPix = _mapper.Map<Pix>(pix);
                    UserDTO userPix = _mapper.Map<UserDTO>(_userService.GetUserById(entityPix.userId));
                    if(checkout.userId != entityPix.userId)
                    {
                        throw new Exception("Os ids de usuário informados estão divergentes.");
                    }
                    if(userPix == null)
                    {
                        throw new Exception("Usuário não encontrado.");
                    }
                    Pix pixReturn = new()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        CPF = entityPix.CPF,
                        pixCode = "shopPrint@gmail.com",
                        userId = userPix.Id,
                        Company = "ShopPrint",
                        userName = userPix.UserName,
                        emissionDate = DateTime.Now,
                        validityDate = DateTime.Now.AddDays(3),
                        paidOutDate = null,
                        value = checkout.Cart.TotalPrice,
                        checkoutId = checkout.Id,
                        paidOut = false
                    };
                    await _paymentCollection.InsertOneAsync(pixReturn);
                    return _mapper.Map<PixDTO>(pixReturn);
                    break;
                case PaymentMethod.BankSlip:
                    BankSlip entityBankSlip = _mapper.Map<BankSlip>(bankSlip);
                    UserDTO userBankSlip = _mapper.Map<UserDTO>(_userService.GetUserById(bankSlip.userId));
                    if(checkout.userId != bankSlip.userId)
                    {
                        throw new Exception("Os ids de usuário informados estão divergentes.");
                    }
                    if(userBankSlip == null)
                    {
                        throw new Exception("Usuário não encontrado.");
                    }
                    BankSlip bankSlipReturn = new()
                    {
                        Id = ObjectId.GenerateNewId().ToString(),
                        CPF = entityBankSlip.CPF,
                        bankSlipCode = "34191.75124 34567.871230 41234.560005 1 93850000026035",
                        userId = userBankSlip.Id,
                        Company = "ShopPrint",
                        userName = userBankSlip.UserName,
                        emissionDate = DateTime.Now,
                        validityDate = DateTime.Now.AddDays(3),
                        paidOutDate = null,
                        value = checkout.Cart.TotalPrice,
                        checkoutId = checkout.Id,
                        paidOut = false
                    };
                    await _paymentCollection.InsertOneAsync(bankSlipReturn);
                    return _mapper.Map<BankSlipDTO>(bankSlipReturn);
                    break;
            }
            throw new Exception("Algo deu errado");
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<object> finalizePayment(string paymentId)
    {
        try
        {
            var exist = await _paymentCollection.Find(c => c.Id == paymentId).FirstOrDefaultAsync();
            if(exist == null)
            {
                throw new Exception("Payment não localizado.");
            }
            if(exist.paidOut == true)
            {
                throw new Exception("Payment já finalizado.");
            }
            exist.paidOut = true;
            exist.paidOutDate = DateTime.Now;
            var checkout = await _checkoutCollection.Find(c => c.Id == exist.checkoutId).FirstOrDefaultAsync();
            checkout.finished = true;
            await _paymentCollection.ReplaceOneAsync(c => c.Id == exist.Id, exist);
            await _checkoutCollection.ReplaceOneAsync(c => c.Id == checkout.Id, checkout);
            return exist.Id;
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
