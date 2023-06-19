using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Models;

namespace ShopPrint_API.Services;
public class CheckoutService
{
    public readonly IMongoCollection<Checkout> _checkoutCollection;
    public readonly IMongoCollection<Payment> _paymentCollection;
    private readonly IMapper _mapper;
    public CheckoutService(IOptions<MongoSettings> mongoSettingsPar, IMapper mapper)
    {
        MongoService mongoSettings = new MongoService(mongoSettingsPar);
        _checkoutCollection = mongoSettings._iMongoDatabase.GetCollection<Checkout>("Checkout");
        _paymentCollection = mongoSettings._iMongoDatabase.GetCollection<Payment>("Payment");
        _mapper = mapper;
    }

    public async Task<object> Create(CheckoutDTO newCheckout)
    {
        try
        {
            Checkout checkout = _mapper.Map<Checkout>(newCheckout);
            checkout.Id = ObjectId.GenerateNewId().ToString();
            await _checkoutCollection.InsertOneAsync(checkout);
            return checkout.Id;                
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    public async Task<IEnumerable<object>> GetAllByUser(string userId)
    {
        try
        {
            IEnumerable<Checkout> checkoutList = await _checkoutCollection.Find(c => c.userId == userId).ToListAsync();
            IEnumerable<CheckoutDTO> checkoutDTOs = checkoutList.Select(mp => _mapper.Map<CheckoutDTO>(mp));
            return checkoutDTOs;
        }
        catch (Exception ex) 
        {
            throw;
        }
    }

    public async Task<object> GetById(string id)
    {
        try
        {
            Checkout checkout = await _checkoutCollection.Find(c => c.Id == id).FirstOrDefaultAsync();
            if (checkout == null)
            {
                throw new Exception("O checkout não foi localizada.");
            }
            CheckoutDTO checkoutDTO = _mapper.Map<CheckoutDTO>(checkout);
            return checkoutDTO;
        }
        catch (Exception ex) 
        {
            throw;
        }
    }

    public async Task<object> Delete(string id)
    {
        try
        {
            Checkout checkout = await _checkoutCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
            if (checkout == null)
            {
                throw new Exception("Checkout não localizado.");
            }
            if(checkout.finished == true)
            {
                throw new Exception("Este checkout já foi finalizado.");   
            }
            await _checkoutCollection.DeleteOneAsync(x => x.Id == id);
            var payment = _paymentCollection.Find(x => x.checkoutId == checkout.Id).FirstOrDefaultAsync();
            if(payment != null)
            {
                await _paymentCollection.DeleteOneAsync(x => x.checkoutId == checkout.Id);
            }
            return "Checkout deletado.";
        }
        catch(Exception ex)
        {
        throw;
        }
    }

    public async Task<object> Update(string Id, CheckoutDTO updateCheckout)
    {
        try
        {
            Checkout checkout = await _checkoutCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
            if (checkout == null)
            {
                throw new Exception("Checkout não localizado.");
            }
            if(checkout.finished == true)
            {
                throw new Exception("Este checkout já foi finalizado.");   
            }
            checkout = _mapper.Map<Checkout>(updateCheckout);
            checkout.Id = Id;
            await _checkoutCollection.ReplaceOneAsync(x => x.Id == checkout.Id, checkout);
            return Id;
        }
        catch(Exception ex)
        {
            throw;
        }
    }
}
