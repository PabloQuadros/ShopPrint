using ShopPrint.Application.DTOs;
using ShopPrint.Domain.Entities;
using ShopPrint.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrint.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<IEnumerable<ProductDTO>> GetProductsByFilter(IEnumerable<string> categories, decimal? minPrice, decimal? maxPrice, SortOption sortOption);
        Task<Product> GetById(int id);
        Task Add (ProductDTO productDTO);
        Task Update (ProductDTO productDTO);
        Task Delete (int id);
    }
}
