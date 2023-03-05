using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopPrint.Domain.Validation.Shared;

namespace ShopPrint.Application.DTOs
{
    public class CategoryDTO
    {
        public int Id { get; set; }

        [NameValidation]
        [DisplayName("Name")]
        public string Name { get; set; }
    }
}
