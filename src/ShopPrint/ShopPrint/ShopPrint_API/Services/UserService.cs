using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopPrint_API.Authentication;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Models;

namespace ShopPrint_API.Services
{

    public class UserService
    {
        public readonly IMongoCollection<User> _userCollection;
        private readonly IMapper _mapper;
        public UserService(IOptions<MongoSettings> mongoSettingsPar,IMapper mapper)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _userCollection = mongoSettings._iMongoDatabase.GetCollection<User>("User");
            _mapper = mapper;
        }

        public async Task<string> CreateUser(UserDTO newUser)
        {
            try
            {
                User user  = await _userCollection.Find(x => x.Email== newUser.Email).FirstOrDefaultAsync();
                if (user!=null) 
                {
                    return "Este e-mail já está cadastrado.";
                }
                user = await _userCollection.Find(x => x.PhoneNumber == newUser.PhoneNumber).FirstOrDefaultAsync();
                if (user != null)
                {
                    return "Este telefone já está cadastrado.";
                }
                user = _mapper.Map<User>(newUser);
                user.Id = ObjectId.GenerateNewId().ToString();
                await _userCollection.InsertOneAsync(user);
                return user.Id;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Object> Login(UserLoginDTO loginUser)
        {
            try
            {
                User user = await _userCollection.Find(x => x.Email == loginUser.Email).FirstOrDefaultAsync();
                if (user==null) 
                {
                    return "E-mail inválido.";
                }
                if(user.Password != loginUser.Password)
                {
                    return "Senha inválida.";
                }
                var token = TokenService.GenerateToken(user);
                var userToken = new UserTokenDTO 
                {
                    UserId = user.Id,
                    Token= token
                };
                return userToken;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Object> DeleteUser(string id, string password)
        {
            try
            {
                User user = await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (user==null)
                {
                    return "Usuário não localizado.";
                }
                if(user.Password != password) 
                {
                    return "Senha inválida.";
                }
                await _userCollection.DeleteOneAsync(x => x.Id == id);
                return "Usuário deletado.";
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Object> GetUserById(string id)
        {
            try
            {
                User user = await _userCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (user == null)
                {
                    return "Usuário não localizado.";
                }
                return user;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Object> UpdateUser(UserDTO updateUser)
        {
            try
            {
                User user = await _userCollection.Find(x => x.Id == updateUser.Id).FirstOrDefaultAsync();
                if (user == null)
                {
                    return "Usuário não localizado.";
                }
                user = await _userCollection.Find(x => x.Email == updateUser.Email).FirstOrDefaultAsync();
                if(user != null && user.Id != updateUser.Id)
                {
                    return "O e-mail fornecido já esta em uso.";
                }
                user = await _userCollection.Find(x => x.PhoneNumber == updateUser.PhoneNumber).FirstOrDefaultAsync();
                if (user!=null && user.PhoneNumber!= updateUser.PhoneNumber)
                {
                    return "O telefone fornecido já está em uso.";
                }
                user = _mapper.Map<User>(updateUser);
                await _userCollection.ReplaceOneAsync(x => x.Id == user.Id, user);
                return user.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
