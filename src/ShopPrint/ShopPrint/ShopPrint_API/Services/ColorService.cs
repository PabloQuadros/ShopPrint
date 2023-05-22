using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.DTOs;
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


        public async Task<object> Create(ColorDTO newColor)
        {
            try
            {
                Color color = await _colorCollection.Find(x => x.Name.ToLower() == newColor.Name.ToLower()).FirstOrDefaultAsync();
                if (color != null)
                {
                    throw new Exception("Já existe uma cor registrada com esse nome.");
                }
                color = _mapper.Map<Color>(newColor);
                color.Id = ObjectId.GenerateNewId().ToString();
                await _colorCollection.InsertOneAsync(color);
                return color.Id;                
            }
            catch (Exception ex)
            {
                throw;
            }
        } 

        public async Task<IEnumerable<ColorDTO>> GetAllColor()
        {
            try
            {
                IEnumerable<Color> colorList = await _colorCollection.Find(c => c.Id != null).ToListAsync();
                IEnumerable<ColorDTO> colorDTOs = colorList.Select(mp => _mapper.Map<ColorDTO>(mp));
                return colorDTOs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> GetColorById(string id)
        {
            try
            {
                Color color = await _colorCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (color == null)
                {
                    throw new Exception("A cor não foi localizada.");
                }
                return _mapper.Map<ColorDTO>(color);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> DeleteColor(string id)
        {
            try
            {
                Color color = await _colorCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (color == null)
                {
                    throw new Exception("Cor não localizado.");
                }
                await _colorCollection.DeleteOneAsync(x => x.Id == id);
                return "Cor deletado.";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> UpdateColor(ColorDTO updateColor)
        {
            Color color = await _colorCollection.Find(x => x.Id == updateColor.Id).FirstOrDefaultAsync();
            if (color == null)
            {
                throw new Exception("Cor não localizado.");
            }
            Color exist = await _colorCollection.Find(x => x.Name.ToLower() == updateColor.Name && x.Id != updateColor.Id).FirstOrDefaultAsync();
            if (exist != null )
            {
                throw new Exception("A cor mencionada já existe.");
            }
            color.Name = updateColor.Name;
            await _colorCollection.ReplaceOneAsync(x => x.Id == color.Id, color);
            return color.Id;
        }
    }
}   
