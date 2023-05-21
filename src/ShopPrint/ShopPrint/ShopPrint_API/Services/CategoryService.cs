using Amazon.Runtime.Internal.Transform;
using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Models;

namespace ShopPrint_API.Services
{
    public class CategoryService
    {
        public readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;
        public CategoryService(IOptions<MongoSettings> mongoSettingsPar, IMapper mapper)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _categoryCollection = mongoSettings._iMongoDatabase.GetCollection<Category>("Category");
            _mapper = mapper;
        }

        public async Task<string> CreateCategory (CategoryDTO newCategory)
        {
            try
            {
                Category category = await _categoryCollection.Find(x => x.Name == newCategory.Name).FirstOrDefaultAsync();
                if (category != null)
                {
                    throw new Exception($"A categoria {category.Name} já está cadastrada.");
                }
                category = _mapper.Map<Category>(newCategory);
                category.Id = ObjectId.GenerateNewId().ToString();
                await _categoryCollection.InsertOneAsync(category);
                return category.Id;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Object> GetCategoryById (string id)
        {
            try
            {
                Category category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (category == null) 
                {
                    throw new Exception("Categoria não localizada.");
                }
                CategoryDTO categoryDto = _mapper.Map<CategoryDTO>(category);
                return categoryDto;
            }
            catch(Exception ex)
            {
                throw;
            }
        }


        public async Task<IEnumerable<Object>> GetAllCategories()
        {
            try
            {
                IEnumerable<Category> categoryList = await _categoryCollection.Find(c => c.Id != null).ToListAsync();
                IEnumerable<CategoryDTO> categoryDTOs = categoryList.Select(mp => _mapper.Map<CategoryDTO>(mp));
                return categoryDTOs;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<string> UpdateCategory(CategoryDTO updateCategory)
        {
            try
            {
                Category category = await _categoryCollection.Find(x => x.Name == updateCategory.Name).FirstOrDefaultAsync();
                if (category != null)
                {
                    return $"A categoria {category.Name} já está cadastrada.";
                }
                category = _mapper.Map<Category>(updateCategory);
                await _categoryCollection.ReplaceOneAsync(x=>x.Id == category.Id, category);
                return category.Id;
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<Object> DeleteCategory(string id)
        {
            try
            {
                Category category = await _categoryCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if (category == null)
                {
                    return "Categoria não localizada.";
                }
                await _categoryCollection.DeleteOneAsync(x => x.Id == category.Id);
                return "Categoria deletada.";
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
