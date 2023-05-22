using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Models;

namespace ShopPrint_API.Services
{
    public class MaterialService 
    {
        public readonly IMongoCollection<Material> _materialCollection;
        private readonly IMapper _mapper;
        public MaterialService(IOptions<MongoSettings> mongoSettingsPar, IMapper mapper)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _materialCollection = mongoSettings._iMongoDatabase.GetCollection<Material>("Material");
            _mapper = mapper;
        }


        public async Task<object> Create(MaterialDTO newMaterial)
        {
            try
            {
                Material material = await _materialCollection.Find(x => x.Name.ToLower() == newMaterial.Name.ToLower()).FirstOrDefaultAsync();
                if (material != null)
                {
                    throw new Exception("Já existe uma cor registrada com esse nome.");
                }
                material = _mapper.Map<Material>(newMaterial);
                material.Id = ObjectId.GenerateNewId().ToString();
                await _materialCollection.InsertOneAsync(material);
                return material.Id;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<MaterialDTO>> GetAllMaterial()
        {
            try
            {
                IEnumerable<Material> materialList = await _materialCollection.Find(c => c.Id != null).ToListAsync();
                IEnumerable<MaterialDTO> materialDTOs = materialList.Select(mp => _mapper.Map<MaterialDTO>(mp));
                return materialDTOs;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> GetMaterialById(string id)
        {
            try
            {
                Material material = await _materialCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (material == null)
                {
                    throw new Exception("O material não foi localizada.");
                }
                return _mapper.Map<MaterialDTO>(material);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> DeleteMaterial(string id)
        {
            try
            {
                Material material = await _materialCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (material == null)
                {
                    throw new Exception("Material não localizado.");
                }
                await _materialCollection.DeleteOneAsync(x => x.Id == id);
                return "Material deletado.";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<object> UpdateMaterial(MaterialDTO updateMaterial)
        {
            Material material = await _materialCollection.Find(x => x.Id == updateMaterial.Id).FirstOrDefaultAsync();
            if (material == null)
            {
                throw new Exception("Material não localizado.");
            }
            Material exist = await _materialCollection.Find(x => x.Name.ToLower() == updateMaterial.Name && x.Id != updateMaterial.Id).FirstOrDefaultAsync();
            if (exist != null)
            {
                throw new Exception("O material mencionada já existe.");
            }
            material.Name = updateMaterial.Name;
            await _materialCollection.ReplaceOneAsync(x => x.Id == material.Id, material);
            return material.Id;
        }
    }
}
