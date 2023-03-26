using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductsCatalogApp.Repositories
{
    public class OrderRepository : PostgresRepository<Order>, IOrderRepository
    {
        private readonly CatalogDbContext _context;

        public OrderRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.UserId == userId)
                .SelectMany(o => o.OrderItems)
                .ToListAsync();
        }
    }
}