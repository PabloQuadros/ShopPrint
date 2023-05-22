using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.Models;

namespace ShopPrint_API.Services
{
    public class ColorService
    {

        public readonly IMongoCollection<Color> _colorCollection;
        private readonly IMapper _mapper;
        public ColorService(IOptions<MongoSettings> mongoSettingsPar, IMapper mapper)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _colorCollection = mongoSettings._iMongoDatabase.GetCollection<Color>("Color");
            _mapper = mapper;
        }

    }
}   
