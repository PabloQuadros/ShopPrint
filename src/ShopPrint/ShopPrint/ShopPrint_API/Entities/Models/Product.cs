using ShopPrint_API.Entities.Validation.Product;
using ShopPrint_API.Entities.Validation.Shared;
using System.ComponentModel.DataAnnotations;

namespace ShopPrint_API.Entities.Models
{
    public class Product
    {
            public string? Id { get; set; }

            [NameValidation]
            public string Name { get; set; }

            [ProductDescriptionValidation]
            public string Description { get; set; }

            [Required]
            [Range(0.1, 1000000, ErrorMessage = "O preço deve estar entre 0,1 e 1.000.000,00.")]
            public double Price { get; set; }

            [Required]
            [Range(0, 1000000, ErrorMessage = "O estoque deve estar entre 0 e 1.000.000.")]
            public int Stock { get; set; }

            [UrlValidation]
            public string Image { get; set; }

            [Required(ErrorMessage = "Necessário informar uma categoria.")]
            public string CategoryName { get; set; }
            [Required(ErrorMessage = "Necessário informar um matrial.")]
            public string Material { get; set; }
            [Required(ErrorMessage = "Necessário informar uma cor.")]
            public string Color { get; set; }

    }
}
