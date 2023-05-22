using ShopPrint_API.Entities.Validation.Shared;

namespace ShopPrint_API.Entities.DTOs
{
    public class MaterialDTO
    {
        public string Id { get; set; }
        [NameValidation]
        public string Material { get; set; }
    }
}
