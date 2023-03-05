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
    public class OrderDTO
    {
        public int Id { get; set; }

        [DisplayName("Hora da Ordem")]
        public DateTime OrderDate { get; set; }

        [DisplayName("Usuário")]
        public int UserId { get; set; }
        
        [JsonIgnore]
        public virtual IUser? User { get; set; }

        [JsonIgnore]
        public ICollection<OrderItem>? OrderItems { get; set; }
    }
}
