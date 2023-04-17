using ShopPrint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ShopPrint.Domain.Validation.User
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false)]
    public class UserPhoneNumberValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            var phoneNumber = value.ToString();

            if (!Regex.IsMatch(phoneNumber, @"^\+[1-9]\d{0,2}\s\d{1,3}\s\d{4,}$"))
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
