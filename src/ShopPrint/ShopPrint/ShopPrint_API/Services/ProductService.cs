using AutoMapper;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using ShopPrint_API.DataBase.Mongo;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Models;

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
            if (product != null)
            {
                throw new Exception($"Já existe um produto cadastrado com o nome: {newProduct.Name}");
            }
            Category category = await _categoryCollection.Find(x => x.Name.ToLower() == newProduct.CategoryName.ToLower()).FirstOrDefaultAsync();
            if (category == null)
            {
                throw new Exception("A categoria informada não existe.");
            }
            Color color = await _colorCollection.Find(x => x.Name.ToLower() == newProduct.Color.ToLower()).FirstOrDefaultAsync();
            if (color == null)
            {
                throw new Exception("A cor informada não existe.");
            }
            Material material = await _materialCollection.Find(x => x.Name.ToLower() == newProduct.Material.ToLower()).FirstOrDefaultAsync();
            if (material == null)
            {
                throw new Exception("O material informado não existe.");
            }
            product = _mapper.Map<Product>(newProduct);
            product.Id = ObjectId.GenerateNewId().ToString();
            product.Material = material.Name;
            product.Color = color.Name;
            product.CategoryName = category.Name;
            await _productCollection.InsertOneAsync(product);
            return product.Id;
        }

        public async Task<Object> GetProductById(string Id)
        {
            Product product = await _productCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
            if (product == null)
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
                IEnumerable<Product> productList = await _productCollection.Find(c => c.Id != null).ToListAsync();
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
                if (product == null)
                {
                    throw new Exception("Produto não localizado.");
                }
                await _productCollection.DeleteOneAsync(x => x.Id == id);
                return "Produto deletado.";
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<string> UpdateProduct(ProductDTO updateProduct)
        {
            Product product = await _productCollection.Find(x => x.Id == updateProduct.Id).FirstOrDefaultAsync();
            if (product == null)
            {
                throw new Exception("Produto não localizado.");
            }
            product = await _productCollection.Find(x => x.Name == updateProduct.Name).FirstOrDefaultAsync();
            if (product.Id != updateProduct.Id && product.Name == updateProduct.Name)
            {
                throw new Exception("Já existe um produto registrado com esse nome.");
            }
            Category category = await _categoryCollection.Find(x => x.Name.ToLower() == updateProduct.CategoryName.ToLower()).FirstOrDefaultAsync();
            if (category == null)
            {
                throw new Exception("A categoria informada não existe.");
            }
            Color color = await _colorCollection.Find(x => x.Name.ToLower() == updateProduct.Color.ToLower()).FirstOrDefaultAsync();
            if (color == null)
            {
                throw new Exception("A cor informada não existe.");
            }
            Material material = await _materialCollection.Find(x => x.Name.ToLower() == updateProduct.Material.ToLower()).FirstOrDefaultAsync();
            if (material == null)
            {
                throw new Exception("O material informado não existe.");
            }
            product = _mapper.Map<Product>(updateProduct);
            product.Material = material.Name;
            product.Color = color.Name;
            product.CategoryName = category.Name;
            await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product);
            return product.Id;
        }


        public async Task<IEnumerable<ProductDTO>> Filter(FilterDTO filter)
        {
            IEnumerable<Product> productList = await _productCollection.Find(c => c.Id != null).ToListAsync();


            if (filter.Color != null && filter.Color.Count() > 0 )
            {
                productList = productList.Where(c => filter.Color.Contains(c.Color)).ToList();
            }

            if (filter.Material != null && filter.Material.Count() > 0)
            {
                productList = productList.Where(c => filter.Material.Contains(c.Material)).ToList();
            }

            if (filter.Category != null && filter.Category.Count() > 0)
            {
                productList = productList.Where(c => filter.Category.Contains(c.CategoryName)).ToList();
            }

            if (filter.minValue != null && filter.minValue >= 0) 
            {
                productList = productList.Where(c => c.Price >= filter.minValue).ToList();
            }

            if (filter.maxValue != null && filter.maxValue >= 0)
            {
                productList = productList.Where(c => c.Price <= filter.maxValue).ToList();
            }

            List<ProductDTO> returnList = new List<ProductDTO>();
            if (productList.Count() > 0)
            {
                foreach (var item in productList)
                {
                    returnList.Add(_mapper.Map<ProductDTO>(item));
                }
            }
            return returnList;
        }

        public async Task<ProductDTO> GetProductByName(string name)
        {
            try
            {
                var item = await _productCollection.Find(x => x.Name.ToLower() == name.ToLower()).FirstOrDefaultAsync();
                if (item == null)
                {
                    throw new Exception("Produto não localizado.");
                }
                return _mapper.Map<ProductDTO>(item);
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
