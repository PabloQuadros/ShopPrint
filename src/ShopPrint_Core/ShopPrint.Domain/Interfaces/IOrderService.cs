using ShopPrint.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopPrint.Domain.Interfaces
{
    public interface IOrderService
    {
        Task<Order> GetOrderByIdAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByUserAsync(int userId);
        Task<Order> CreateOrderAsync(int userId, IEnumerable<OrderItem> orderItems);
        Task<bool> UpdateOrderAsync(Order order);
        Task DeleteOrderAsync(int id);

    }
}
