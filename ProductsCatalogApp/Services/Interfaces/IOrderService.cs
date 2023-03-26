using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;

namespace ProductsCatalogApp.Services
{
    public interface IOrderService
    {
        Task<Order> GetAsync(int id);
        Task<List<Order>> GetAllAsync();
        Task<Order> CreateAsync(Order order);
        Task<Order> UpdateAsync(int id, Order order);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<OrderItem>> GetOrderItemsByUserId(int id);
    }
}