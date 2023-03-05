using ShopPrint.Domain.Validation.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrint.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [NameValidation]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
