using Microsoft.AspNetCore.Routing.Constraints;

namespace ShopPrint_API.Entities.DTOs
{
    public class FilterDTO
    {
        public double? minValue { get; set; }
        public double? maxValue { get; set; }
        public string? Category { get; set; }
        public string? Color { get; set; }
        public string? Material { get; set; }
    }
}
