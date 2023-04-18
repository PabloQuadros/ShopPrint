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
        private readonly IMapper _mapper;
        public ProductService(IOptions<MongoSettings> mongoSettingsPar, IMapper mapper)
        {
            MongoService mongoSettings = new MongoService(mongoSettingsPar);
            _productCollection = mongoSettings._iMongoDatabase.GetCollection<Product>("Product");
            _mapper = mapper;
        }

        public async Task<string> CreateProduct(ProductDTO newProduct)
        {
            Product product = await _productCollection.Find(x => x.Name == newProduct.Name).FirstOrDefaultAsync();
            if(product != null)
            {
                return $"Já existe um produto cadastrado com o nome: {newProduct.Name}";
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
                return "Produto nao localizado";
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
                    return "Produto não localizado.";
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
                return "Produto não localizado.";
            }
            product = await _productCollection.Find(x => x.Name == updateProduct.Name).FirstOrDefaultAsync();
            if(product.Id != updateProduct.Id && product.Name == updateProduct.Name) 
            {
                return "Já existe um produto registrado com esse nome.";
            }
            product = _mapper.Map<Product>(updateProduct);
            await _productCollection.ReplaceOneAsync(x => x.Id == product.Id, product);
            return product.Id;
        }
    }
}
