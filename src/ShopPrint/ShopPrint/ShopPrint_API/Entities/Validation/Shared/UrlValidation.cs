using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShopPrint_API.Entities.Validation.Shared
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class UrlValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var url = value.ToString();

            if (!Regex.IsMatch(url, @"^(http|https)://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?$"))
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string field)
        {
            return "Por favor, insira uma URL válida.";
        }

    }
}
