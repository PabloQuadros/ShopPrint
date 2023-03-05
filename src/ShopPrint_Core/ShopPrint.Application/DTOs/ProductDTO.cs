using ShopPrint.Domain.Entities;
using ShopPrint.Domain.Validation.Product;
using ShopPrint.Domain.Validation.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrint.Application.DTOs
{
    public class ProductDTO
    {
        public int Id { get; set; }

        [NameValidation]
        [DisplayName("Nome do Produto")]
        public string Name { get; set; }

        [ProductDescriptionValidation]
        [DisplayName("Descrição")]
        public string Description { get; set; }

        [Required]
        [Range(0.1, 1000000, ErrorMessage = "O preço deve estar entre 0,1 e 1.000.000,00.")]
        [DisplayName("Preço")]
        public double Price { get; set; }

        [Required]
        [Range(0, 1000000, ErrorMessage = "O estoque deve estar entre 0 e 1.000.000.")]
        [DisplayName("Estoque")]
        public int Stock { get; set; }

        [UrlValidation]
        [DisplayName("Url da Imagem")]
        public string Image { get; set; }

        [Required(ErrorMessage = "Necessário informar uma categoria.")]
        [DisplayName("Categoria")]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }
    }
}
