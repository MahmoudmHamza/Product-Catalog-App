using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsCatalogApp.Models;

namespace ProductsCatalogApp.Repositories
{
    public class UserRepository : PostgresRepository<User>, IUserRepository
    {
        private readonly CatalogDbContext _context;

        public UserRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetRecommendedProductsAsync(int id)
        {
            // Get the user's order history
            var userOrders = await _context.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(od=>od.Product)
                .Where(o=>o.UserId == id).ToListAsync();

            // Get a list of all product IDs the user has purchased
            var purchasedProductIds = userOrders
                .SelectMany(o => o.OrderItems)
                .Select(od => od.ProductId)
                .Distinct()
                .ToList();

            // Get a list of recommended products based on user's purchase history
            var recommendedProducts = await _context.Products
                .Include(p => p.OrderItems)
                .Where(p => !purchasedProductIds.Contains(p.Id))
                .OrderByDescending(p => p.OrderItems.Count(od => purchasedProductIds.Contains(od.ProductId)))
                .Take(10)
                .ToListAsync();

            return recommendedProducts;
        }
            
    }
}