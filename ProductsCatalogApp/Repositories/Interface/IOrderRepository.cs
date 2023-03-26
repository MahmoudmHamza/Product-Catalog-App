using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;

namespace ProductsCatalogApp.Repositories
{
    public interface IOrderRepository
    {
        // Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);
        // Task<IEnumerable<Product>> GetRecommendedProductsAsync(int userId);
        // Task<IEnumerable<Order>> GetUserOrderHistoryAsync(int userId);
        Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(int userId);
    }
}