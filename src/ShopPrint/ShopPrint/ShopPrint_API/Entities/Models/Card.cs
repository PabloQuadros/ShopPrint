using System.ComponentModel.DataAnnotations;

namespace ShopPrint_API.Entities.Models
{
    public class Card: Payment
    {
        [RegularExpression(@"^\d{16}$", ErrorMessage = "O número do cartão deve ser um número de 16 dígitos.")]
        public string cardNumber {  get; set; }
        [RegularExpression(@"^\d{3}$", ErrorMessage = "CVV deve ser um número de 3 dígitos.")]
        public string CVV { get; set; }
        [RegularExpression(@"^(0[1-9]|1[0-2])/\d{2}$", ErrorMessage = "Validity card must be in the format MM/YY.")]
        public string validityCard { get; set; }
        [RegularExpression(@"^[A-Za-z\s]+$", ErrorMessage = "O nome do cartão deve conter apenas letras.")]
        public string CardName { get; set; }
        public bool Credit { get; set; }
        [RegularExpression(@"^[1-9]$|^[1][0-2]$", ErrorMessage = "As parcelas devem ser um número inteiro entre 1 e 12.")]
        public int Installments { get; set; }
    }
}
