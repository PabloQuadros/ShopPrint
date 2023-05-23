namespace ShopPrint_API.Entities.Models
{
    public class BankSlip: Paymant
    {
        public string CPF { get; set; }
        public string bankSlipCode { get; set; }
    }
}
