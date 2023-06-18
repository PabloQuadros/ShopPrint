namespace ShopPrint_API.Entities.DTOs;
public class BankSlipDTO : PaymentDTO
{
    public string CPF { get; set; }
    public string? bankSlipCode { get; set; }
}
