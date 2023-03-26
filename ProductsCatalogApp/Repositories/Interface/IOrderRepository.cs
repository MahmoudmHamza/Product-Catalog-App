using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;

namespace ProductsCatalogApp.Repositories
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(int userId);
    }
}