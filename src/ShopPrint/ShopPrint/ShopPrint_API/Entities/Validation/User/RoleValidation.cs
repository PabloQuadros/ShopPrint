using System.ComponentModel.DataAnnotations;

namespace ShopPrint_API.Entities.Validation.User
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class RoleValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var role = value.ToString();

            if (role != "Admin" && role != "User")
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

