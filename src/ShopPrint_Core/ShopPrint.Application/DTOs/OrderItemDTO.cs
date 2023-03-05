using ShopPrint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ShopPrint.Application.DTOs
{
    public class OrderItemDTO
    {
        public int Id { get; set; }
        
        [DisplayName("Quantidade")]
        public int Quantity { get; set; }
        [DisplayName("Preço")]
        public decimal Price { get; set; }
        [DisplayName("Produto")]
        public int ProductId { get; set; }

        [JsonIgnore]
        public virtual Product? Product { get; set; }
        [DisplayName("Número da Ordem")]
        public int OrderId { get; set; }
        [JsonIgnore]
        public virtual Order? Order { get; set; }
    }
}
