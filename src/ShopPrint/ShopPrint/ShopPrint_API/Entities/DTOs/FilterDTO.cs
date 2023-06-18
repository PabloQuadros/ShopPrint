using Microsoft.AspNetCore.Routing.Constraints;

namespace ShopPrint_API.Entities.DTOs
{
    public class FilterDTO
    {
        public double? minValue { get; set; }
        public double? maxValue { get; set; }
        public List<string>? Category { get; set; }
        public List<string>? Color { get; set; }
        public List<string>? Material { get; set; }
    }
}
