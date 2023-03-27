using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;

namespace ProductsCatalogApp.Repositories
{
    public interface IOrderRepository : IPostgresRepository<Order>
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(int userId);
    }
}