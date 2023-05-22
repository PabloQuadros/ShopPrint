using ShopPrint_API.Entities.Validation.Shared;

namespace ShopPrint_API.Entities.DTOs
{
    public class ColorDTO
    {
        public string Id { get; set; }
        [NameValidation]
        public string Name { get; set; }
    }
}
