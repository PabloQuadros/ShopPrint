using ShopPrint_API.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace ShopPrint_API.Entities.Models
{
    public class Checkout
    {
        public string? Id { get; set; }
        public string userId { get; set; }
        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        [RegularExpression(@"^\d{5}-\d{3}$", ErrorMessage = "O formato do CEP é inválido.")]
        public string Cep { get; set; }

        [Required(ErrorMessage = "O campo Rua é obrigatório.")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "O formato da estrada é inválido.")]
        public string Street { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "O formato da cidade é inválida.")]
        public string City { get; set; }

        [Required(ErrorMessage = "O campo Província é obrigatório.")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "O formato do estado é inválido.")]
        public string Province { get; set; }

        [Required(ErrorMessage = "O campo País é obrigatório.")]
        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "O formato do país é inválido.")]
        public string Country { get; set; }

        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "O formato do Complemento é inválido.")]
        public string? Complemenent { get; set; }

        [RegularExpression(@"^[A-Za-z0-9\s]+$", ErrorMessage = "O formato da Referência é inválido.")]
        public string? Reference { get; set; }
        public Cart Cart { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool finished { get; set; }
    }
}
