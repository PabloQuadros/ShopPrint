using ShopPrint.Domain.Validation.Shared;
using ShopPrint.Domain.Validation.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShopPrint.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

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

        [Required(ErrorMessage ="Necessário informar uma categoria.")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
        //public ICollection<OrderItem> OrderItems { get; set; }
    }
}
