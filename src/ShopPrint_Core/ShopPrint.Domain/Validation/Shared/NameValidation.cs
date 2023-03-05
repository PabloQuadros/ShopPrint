using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShopPrint.Domain.Validation.Shared
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class NameValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
           if(value == null)
           {
               return false;
           }

           var name = value.ToString();

           if(!Regex.IsMatch(name, @"^[a-zA-Z0-9 ]{1,15}$"))
           {
               return false;
           }

          return true;
        }

        public override string FormatErrorMessage(string field)
        {
            return $"O campo {field} não pode ser nulo e deve conter no máximo 15 caracteres,podendo ser: letras, números e espaços.;";
        }
    }
}
