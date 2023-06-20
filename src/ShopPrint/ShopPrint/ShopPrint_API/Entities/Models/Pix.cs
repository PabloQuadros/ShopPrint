using System.ComponentModel.DataAnnotations;

namespace ShopPrint_API.Entities.Models
{
    public class Pix : Payment
    {
        [Required(ErrorMessage = "O campo CPF é obrigatório.")]
        [RegularExpression(@"^\d{3}\.\d{3}\.\d{3}-\d{2}$", ErrorMessage = "O formato do CPF é inválido.")]
        public string CPF { get; set; }
        public string pixCode { get; set; }

    }
}
