using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProductsCatalogApp.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly CatalogDbContext _context;

        public OrderRepository(CatalogDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Order>> GetAll()
        {
            try
            {
                return await _context.Set<Order>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve all data for entity {typeof(Order)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<Order> Get(int id)
        {
            try
            {
                return await _context.Set<Order>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve data with id: {id} for entity {typeof(Order)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<Order> Add(Order entity)
        {
            try
            {
                await _context.Set<Order>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't create new entry for entity {typeof(Order)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<Order> Update(int id, Order entity)
        {
            try
            {
                Order fetchedEntity = await _context.Set<Order>().FindAsync(id);
                if (fetchedEntity == null)
                {
                    return null;
                }
                
                _context.Set<Order>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't update entry for entity {typeof(Order)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                Order entity = await _context.Set<Order>().FindAsync(id);
                if (entity == null)
                {
                    return false;
                }
                
                _context.Set<Order>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't delete entry for entity {typeof(Order)} \n ErrorDetails: {ex.Message}");
            }
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