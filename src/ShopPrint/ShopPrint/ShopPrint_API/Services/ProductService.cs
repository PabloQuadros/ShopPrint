using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Models;
using System.Runtime.ConstrainedExecution;

namespace ShopPrint_API.Services
{
    public class ProductService
    {
        public readonly IMongoCollection<Product> _productCollection;
        public readonly IMongoCollection<Category> _categoryCollection;
        public readonly IMongoCollection<Color> _colorCollection;
        public readonly IMongoCollection<Material> _materialCollection;
        private readonly IMapper _mapper;
        public ProductService(IOptions<MongoSettings> mongoSettingsPar, IMapper mapper)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _productCollection = mongoSettings._iMongoDatabase.GetCollection<Product>("Product");
            _categoryCollection = mongoSettings._iMongoDatabase.GetCollection<Category>("Category");
            _colorCollection = mongoSettings._iMongoDatabase.GetCollection<Color>("Color");
            _materialCollection = mongoSettings._iMongoDatabase.GetCollection<Material>("Material");
            _mapper = mapper;
        }

        public async Task<string> CreateProduct(ProductDTO newProduct)
        {
            Product product = await _productCollection.Find(x => x.Name == newProduct.Name).FirstOrDefaultAsync();
            if(product != null)
            {
                throw new Exception($"Já existe um produto cadastrado com o nome: {newProduct.Name}");
            }
            product = _mapper.Map<Product>(newProduct);
            product.Id = ObjectId.GenerateNewId().ToString();
            await _productCollection.InsertOneAsync(product);
            return product.Id;
        }

        public async Task<Object> GetProductById(string Id)
        {
            Product product = await _productCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
            if(product == null )
            {
                throw new Exception("Produto nao localizado");
            }
            ProductDTO productDto = _mapper.Map<ProductDTO>(product);
            return productDto;
        }

        public async Task<IEnumerable<Object>> GetAllProducts()
        {
            try
            {
                IEnumerable<Product> productList  = await _productCollection.Find(c => c.Id != null).ToListAsync();
                IEnumerable<ProductDTO> productDtos = productList.Select(mp => _mapper.Map<ProductDTO>(mp));
                return productDtos;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Object> DeleteProduct(string id)
        {
            try
            {
                Product product = await _productCollection.Find(x => x.Id == id).FirstOrDefaultAsync();
                if(product == null)
                {
                    throw new Exception("Produto não localizado.");
                }
                await _productCollection.DeleteOneAsync(x => x.Id == id);
                return "Produto deletado.";
            }
            catch(Exception ex)
            {
                throw;
            }
        }

        public async Task<string> UpdateProduct(ProductDTO updateProduct)
        {
            Product product = await _productCollection.Find(x => x.Id == updateProduct.Id).FirstOrDefaultAsync();
            if(product == null)
            {
                throw new Exception("Produto não localizado.");
            }
            product = await _productCollection.Find(x => x.Name == updateProduct.Name).FirstOrDefaultAsync();
            if(product.Id != updateProduct.Id && product.Name == updateProduct.Name) 
            {
                throw new Exception("Já existe um produto registrado com esse nome.");
            }
            product = _mapper.Map<Product>(updateProduct);
            await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product);
            return product.Id;
        }


        public async Task<IEnumerable<ProductDTO>> Filter(FilterDTO filter)
        {
            var filterBuilder = Builders<Product>.Filter;
            var filters = new List<FilterDefinition<Product>>();

            if (filter.Color.Count > 0)
            {
                var colorFilter = filterBuilder.In(x => x.Color,filter.Color);
                filters.Add(colorFilter);
            }

            if (filter.Material.Count> 0)
            {
                var materialFilter = filterBuilder.In(x => x.Material, filter.Material);
                filters.Add(materialFilter);
            }

            if (filter.Category.Count > 0)
            {
                var categoryFilter = filterBuilder.In(x => x.CategoryName, filter.Category);
                filters.Add(categoryFilter);
            }

            if (filter.minValue.HasValue && filter.minValue >= 0)
            {
                var minValueFilter = filterBuilder.Lte(x => x.Price, filter.minValue);
                filters.Add(minValueFilter);
            }

            if (filter.maxValue.HasValue && filter.maxValue >= 0)
            {
                var maxValueFilter = filterBuilder.Gte(x => x.Price, filter.maxValue);
                filters.Add(maxValueFilter);
            }

            var combinedFilter = filterBuilder.And(filters);

            var results = _productCollection.Find(combinedFilter).ToList();

            List<ProductDTO> returnList = new List<ProductDTO>();
            if(results.Count > 0) 
            {
                foreach (var item in results)
                {
                    returnList.Add(_mapper.Map<ProductDTO>(item));
                }
            }
            return returnList;
        }
    }
}
