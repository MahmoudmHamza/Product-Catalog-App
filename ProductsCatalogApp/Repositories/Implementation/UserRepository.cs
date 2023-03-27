using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProductsCatalogApp.Models;

namespace ProductsCatalogApp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly CatalogDbContext _context;

        public UserRepository(CatalogDbContext context)
        {
            _context = context;
        }
        
                public async Task<IEnumerable<User>> GetAll()
        {
            try
            {
                return await _context.Set<User>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve all data for entity {typeof(User)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<User> Get(int id)
        {
            try
            {
                return await _context.Set<User>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve data with id: {id} for entity {typeof(User)} " +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }

        public async Task<User> Add(User entity)
        {
            try
            {
                await _context.Set<User>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't create new entry for entity {typeof(User)} " +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }

        public async Task<User> Update(int id, User entity)
        {
            try
            {
                var fetchedEntity = await _context.Set<User>().FindAsync(id);
                if (fetchedEntity == null)
                {
                    return null;
                }
                
                _context.Set<User>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't update entry for entity {typeof(User)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _context.Set<User>().FindAsync(id);
                if (entity == null)
                {
                    return false;
                }
                
                _context.Set<User>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't delete entry for entity {typeof(User)} " +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }

        public async Task<List<Product>> GetRecommendedProductsAsync(int id)
        {
            try
            {
                // Get the user's order history
                var userOrders = await _context.Orders
                    .Include(o => o.OrderItems)
                    .ThenInclude(od=>od.Product)
                    .Where(o=>o.User.Id == id).ToListAsync();

                // Get a list of all product IDs the user has purchased
                var purchasedProductIds = userOrders
                    .SelectMany(o => o.OrderItems)
                    .Select(od => od.Product.Id)
                    .Distinct()
                    .ToList();

                // Get a list of recommended products based on user's purchase history
                var recommendedProducts = await _context.Products
                    .Include(p => p.OrderItems)
                    .Where(p => !purchasedProductIds.Contains(p.Id))
                    .OrderByDescending(p => p.OrderItems.Count(od => purchasedProductIds.Contains(od.Product.Id)))
                    .Take(10)
                    .ToListAsync();

                return recommendedProducts;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve recommended products for user with id: {id}" +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }
    }
}