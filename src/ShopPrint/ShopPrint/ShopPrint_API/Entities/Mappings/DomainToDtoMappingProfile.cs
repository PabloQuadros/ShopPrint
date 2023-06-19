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
            CreateMap<Product,ProductDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Cart, CartDTO>().ReverseMap();
            CreateMap<CartItem, CartItemDTO>().ReverseMap();
            CreateMap<Color, ColorDTO>().ReverseMap();
            CreateMap<Material,MaterialDTO>().ReverseMap();
            CreateMap<Checkout,CheckoutDTO>().ReverseMap();
            CreateMap<Pix, PixDTO>().ReverseMap();
            CreateMap<BankSlip, BankSlipDTO>().ReverseMap();
        }
    }
}
