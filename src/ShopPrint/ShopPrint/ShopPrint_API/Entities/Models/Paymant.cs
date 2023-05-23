namespace ShopPrint_API.Entities.Models
{
    public abstract class Paymant
    {
        public string userId { get; set; }
        public string Company { get; set; }
        public string userName { get; set; }
        public DateTime emissionDate { get; set; }
        public DateTime validityDate { get; set; }
        public DateTime paidOutDate { get; set; }
        public double value { get; set; }
        public bool paidOut { get; set; }
    }
}
