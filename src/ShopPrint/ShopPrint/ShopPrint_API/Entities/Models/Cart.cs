namespace ShopPrint_API.Entities.Models
{
    public class Cart
    {
        public string? Id { get; set; }
        public string? UserId { get; set; }
        public List<CartItem> Items { get; set; }
        public Double TotalPrice { get; set; }
    }
}
