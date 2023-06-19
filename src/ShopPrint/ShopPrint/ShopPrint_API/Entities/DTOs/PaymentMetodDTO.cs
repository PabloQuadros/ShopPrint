namespace ShopPrint_API.Entities.DTOs
{
    public class PaymentMetodDTO
    {
        public PixDTO? pix { get; set; }
        public BankSlipDTO? bankSlip { get; set; }
    }
}
