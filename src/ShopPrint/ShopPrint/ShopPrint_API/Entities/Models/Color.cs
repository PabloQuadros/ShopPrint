using ShopPrint_API.Entities.Validation.Shared;
using System.ComponentModel.DataAnnotations;

namespace ShopPrint_API.Entities.Models
{
    public class Color
    {
        public string Id { get; set; }
        [NameValidation]
        public string Name { get; set; }
    }
}
