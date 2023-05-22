using ShopPrint_API.Entities.Validation.Shared;

namespace ShopPrint_API.Entities.Models
{
    public class Material
    {
        public string Id { get; set; }
        [NameValidation]
        public string Name { get; set; }
    }
}
