namespace ShopPrint_API.Entities.DTOs;
public class PixDTO : PaymentDTO
{
    public string CPF { get; set; }
    public string? pixCode { get; set; }
}
