using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace ShopPrint_API.Entities.Validation.User
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class PhoneNumberValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var phoneNumber = value.ToString();

            if (!Regex.IsMatch(phoneNumber, @"^\+55\s\d{2}\s\d{5}\-\d{4}$"))
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
