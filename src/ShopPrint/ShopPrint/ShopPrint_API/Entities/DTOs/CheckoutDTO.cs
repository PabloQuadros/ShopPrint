
using ShopPrint_API.Entities.Enums;

namespace ShopPrint_API.Entities.DTOs;
public class CheckoutDTO
{
    public string? Id { get; set; }
        public string userId { get; set; }
        public string Cep { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Province { get; set; }
        public string Country { get; set; }
        public string? Complemenent { get; set; }
        public string? Reference { get; set; }
        public CartDTO Cart { get; set; }
        public PaymentMethod PaymentMethod { get; set; }
        public bool finished { get; set; }
}
