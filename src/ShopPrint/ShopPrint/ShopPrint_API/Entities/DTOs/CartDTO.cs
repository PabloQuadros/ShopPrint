using ShopPrint_API.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopPrint_API.Entities.DTOs
{
    public class CartDTO
    {
        public string? Id { get; set; }
        [Required(ErrorMessage = "Necessário informar um usuário para ser atrelado ao carrinho.")]
        public string UserId { get; set; }
        public List<CartItem>? Items { get; set; }
        public Double TotalPrice { get; set; }
    }
}
