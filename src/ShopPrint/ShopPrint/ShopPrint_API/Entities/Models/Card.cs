namespace ShopPrint_API.Entities.Models
{
    public class Card: Paymant
    {
        public string cardNumber {  get; set; }
        public string CVV { get; set; }
        public string validityCard { get; set; }
        public string CardName { get; set; }
        public bool Credit { get; set; }
        public int Installments { get; set; }
    }
}
