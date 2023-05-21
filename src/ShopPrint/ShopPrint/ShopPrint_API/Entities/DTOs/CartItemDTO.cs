using ShopPrint_API.Entities.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopPrint_API.Entities.DTOs
{
    public class CartItemDTO
    {
        [Required(ErrorMessage = "Necessário informar um carrinho para ser atrelado ao produto.")]
        public string CartId { get; set; }
        [Required(ErrorMessage = "Necessário informar um produto para ser colocado no carrinho.")]
        public string ProductId { get; set; }
        public Product? Product { get; set; }
        [Required]
        [Range(1 , 1000,  ErrorMessage = "O estoque deve estar entre 1 e 1.000")]
        public int Quantity { get; set; }
        public double Price { get; set; }
    }
}
.