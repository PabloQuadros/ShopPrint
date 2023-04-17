using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShopPrint_API.Entities.Validation.User
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class EmailValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var email = value.ToString();

            if (!Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                return false;
            }
            return true;
        }
        public override string FormatErrorMessage(string field)
        {
            return $"O campo {field} está inválido.";
        }
    }
}
