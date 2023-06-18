using System.Security.Cryptography;
using AutoMapper;
using Microsoft.Extensions.Options;
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

    public async Task<object> Pay(string checkoutId, PixDTO? pix, CardDTO? card, BankSlipDTO? banckSlip)
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
            switch(checkout.PaymentMethod)
            {
                case PaymentMethod.Pix:
                    Pix entity = _mapper.Map<Pix>(pix);
                    UserDTO user = _mapper.Map<UserDTO>(_userService.GetUserById(entity.userId));
                    if(checkout.userId != entity.userId)
                    {
                        throw new Exception("Os ids de usuário informados estão divergentes.");
                    }
                    if(user == null)
                    {
                        throw new Exception("Usuário não encontrado.");
                    }
                    Pix pixReturn = new()
                    {
                        CPF = entity.CPF,
                        pixCode = "shopPrint@gmail.com",
                        userId = user.Id,
                        Company = "ShopPrint",
                        userName = user.UserName,
                        emissionDate = DateTime.Now,
                        validityDate = DateTime.Now.AddDays(3),
                        paidOutDate = null,
                        value = checkout.Cart.TotalPrice,
                        checkoutId = checkout.Id,
                        paidOut = false
                    };
                    _paymentCollection.InsertOneAsync(pixReturn);
                    return pixReturn;
                    break;
                case PaymentMethod.Debit:
                    break;
                case PaymentMethod.Credit:
                    break;
                case PaymentMethod.BankSlip:
                    break;
            }
            return null;
        }
        catch (Exception ex)
        {
            throw;
        }
    }

}
