using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShopPrint_API.Entities.Validation.User
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class UserNameValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var name = value.ToString();

            if (!Regex.IsMatch(name, @"^[a-zA-Z ]{3,50}$"))
            {
                return false;
            }

            return true;
        }

        public override string FormatErrorMessage(string field)
        {
            return $"O campo {field} não pode ser nulo e deve conter no máximo 50 caracteres,podendo ser: letras e espaços.;";
        }
    }
}
