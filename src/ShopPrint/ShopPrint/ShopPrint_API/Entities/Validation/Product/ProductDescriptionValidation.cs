using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShopPrint_API.Entities.Validation.Product
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class ProductDescriptionValidation : ValidationAttribute
    {

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var description = value.ToString();

            if (!Regex.IsMatch(description, @"^[a-zA-Z0-9 ]{1,200}$"))
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string field)
        {
            return $"O campo {field} deve conter no máximo 200 caracteres.;";
        }
    }
}
