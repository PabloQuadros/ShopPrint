using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Models;
using System.Security;

namespace ShopPrint_API.Services
{
    public class CartService
    {
        public readonly IMongoCollection<Cart> _cartCollection;
        public readonly IMongoCollection<Product> _productCollection;
        public readonly IMongoCollection<User> _userCollection;
        private readonly IMapper _mapper;
        public CartService(IOptions<MongoSettings> mongoSettingsPar, IMapper mapper)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _cartCollection = mongoSettings._iMongoDatabase.GetCollection<Cart>("Cart");
            _productCollection = mongoSettings._iMongoDatabase.GetCollection<Product>("Product");
            _userCollection = mongoSettings._iMongoDatabase.GetCollection<User>("User");
            _mapper = mapper;
        }

        public async Task<string> CreateCart(CartDTO newCart)
        {
            try
            {
                User user = await _userCollection.Find(x => x.Id == newCart.UserId).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception("Usuário não localizado.");
                }
                Cart cart = await _cartCollection.Find(x => x.UserId == newCart.UserId).FirstOrDefaultAsync();
                if (cart != null)
                {
                    throw new Exception ("Este usuário já possuí um carrinho.");
                }
                cart = _mapper.Map<Cart>(newCart);
                cart.Id = ObjectId.GenerateNewId().ToString();
                await _cartCollection.InsertOneAsync(cart);
                return cart.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> AddItem(string productId,  string userId)
        {
            try
            {
                User user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception("Usuário não localizado.");
                }
                Product product = await _productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
                if (product == null) 
                {
                    throw new Exception("Produto não localizado.");
                }
                Cart cart = await _cartCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
                if (product.Stock == 0)
                {
                    throw new Exception("Produto Indisponível.");
                }
                if (cart == null)
                {
                    CartDTO newCart = new CartDTO
                    {
                        Id = null,
                        UserId = userId,
                        Items = null,
                        TotalPrice = 0,
                    };
                    await CreateCart(newCart);
                    cart = await _cartCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
                }
                foreach(var item in cart.Items)
                {
                    if(item.ProductId == productId)
                    {
                        item.Quantity += 1;
                        product.Stock += 1;
                        await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product);
                        item.Price = item.Quantity * item.Product.Price;
                        cart.TotalPrice += item.Price;
                        await _cartCollection.ReplaceOneAsync(x => x.Id == cart.Id, cart);
                        return "Item adicionado com sucesso.";
                    }
                }
                CartItem cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductId = productId,
                    Product = product,
                    Quantity = 1,
                    Price = product.Price
                };
                cart.Items.Add(cartItem);
                product.Stock += 1;
                await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product);
                cart.TotalPrice += cartItem.Price;
                await _cartCollection.ReplaceOneAsync(x => x.Id == cart.Id, cart);
                return "Item adicionado com sucesso.";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> RemoveItem(string productId, string userId)
        {
            try
            {
                User user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception("Usuário não localizado.");
                }
                Product product = await _productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
                if (product == null)
                {
                    throw new Exception("Produto não localizado.");
                }
                Cart cart = await _cartCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
                if (cart == null)
                {
                    throw new Exception("O usuário não possuí um carrinho");
                }
                foreach (var item in cart.Items)
                {
                    if (item.ProductId == productId)
                    {
                        item.Quantity -= 1;
                        product.Stock += 1;
                        await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product);
                        item.Price -= product.Price;
                        if (item.Quantity == 0)
                        {
                            cart.Items.Remove(item);
                        }
                        if (cart.Items.Count == 0)
                        {
                            await _cartCollection.DeleteOneAsync(x => x.Id == cart.Id);
                            return "Item removido com sucesso.";
                        }
                        if (cart.Items.Count > 0)
                        {
                            cart.TotalPrice -= product.Price;
                        }
                        await _cartCollection.ReplaceOneAsync(x => x.Id == cart.Id, cart);
                        return "Item removido com sucesso.";
                    }
                }
                return "O item informado não está no carrinho.";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> GetCartByUserId(string userId)
        {
            User user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Usuário não localizado.");
            }
            Cart cart = await _cartCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (cart == null)
            {
                throw new Exception("O usuário não possuí um carrinho");
            }
            return cart;
        }

        public async Task<object> RemoveProductOfCart(string productId, string userId)
        {
            try
            {
                User user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();
                if (user == null)
                {
                    throw new Exception("Usuário não localizado.");
                }
                Product product = await _productCollection.Find(x => x.Id == productId).FirstOrDefaultAsync();
                if (product == null)
                {
                    throw new Exception("Produto não localizado.");
                }
                Cart cart = await _cartCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
                if (cart == null)
                {
                    throw new Exception("O usuário não possuí um carrinho");
                }
                foreach (var item in cart.Items)
                {
                    if (item.ProductId == productId)
                    {
                        product.Stock += item.Quantity;
                        await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product);
                        cart.Items.Remove(item);
                        if (cart.Items.Count == 0)
                        {
                            await _cartCollection.DeleteOneAsync(x => x.Id == cart.Id);
                            return "Item removido com sucesso.";
                        }
                        if (cart.Items.Count > 0)
                        {
                            cart.TotalPrice -= product.Price;
                        }
                        await _cartCollection.ReplaceOneAsync(x => x.Id == cart.Id, cart);
                        return "Item removido com sucesso.";
                    }
                }
                return "O item informado não está no carrinho.";
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<object> DeleteCart(string userId)
        {
            User user = await _userCollection.Find(x => x.Id == userId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("Usuário não localizado.");
            }
            Cart cart = await _cartCollection.Find(x => x.UserId == userId).FirstOrDefaultAsync();
            if (cart == null)
            {
                throw new Exception("O usuário não possuí um carrinho");
            }
            await _cartCollection.DeleteOneAsync(x => x.Id == cart.Id);
            return "Carrinho excluído com sucesso.";
        }
    }
}
