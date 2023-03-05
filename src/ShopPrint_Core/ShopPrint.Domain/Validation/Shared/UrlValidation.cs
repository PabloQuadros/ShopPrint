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
    public class UrlValidation : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if(value == null)
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
