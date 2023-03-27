using System;
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
            try
            {
                return await _context.Orders
                    .Where(o => o.User.Id == userId)
                    .SelectMany(o => o.OrderItems)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve all orders for user with id: {userId}" +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }
    }
}