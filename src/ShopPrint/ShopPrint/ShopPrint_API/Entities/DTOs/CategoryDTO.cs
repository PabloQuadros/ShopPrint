using ShopPrint_API.Entities.Validation.Shared;
using System.Text.Json.Serialization;

namespace ShopPrint_API.Entities.DTOs
{
    public class CategoryDTO
    {
        public string? Id { get; set; }

        [NameValidation]
        public string Name { get; set; }
    }
}
