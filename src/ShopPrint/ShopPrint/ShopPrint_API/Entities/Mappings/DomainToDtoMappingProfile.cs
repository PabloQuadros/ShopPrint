using AutoMapper;
using ShopPrint_API.Entities.DTOs;
using ShopPrint_API.Entities.Models;

namespace ShopPrint_API.Entities.Mappings
{
    public class DomainToDtoMappingProfile :Profile
    {
        public DomainToDtoMappingProfile() 
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Product,ProdutDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
        }
    }
}
