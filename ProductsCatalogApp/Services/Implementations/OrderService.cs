using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;
using ProductsCatalogApp.Repositories;
using System.Linq;

namespace ProductsCatalogApp.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<Order> GetAsync(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<List<Order>> GetAllAsync()
        {
            var orders = await _repository.GetAll();
            return orders.ToList();
        }

        public async Task<Order> CreateAsync(Order order)
        {
            return await _repository.Add(order);
        }

        public async Task<Order> UpdateAsync(int id, Order order)
        {
            return await _repository.Update(id, order);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<IEnumerable<OrderItem>> GetOrderItemsByUserId(int id)
        {
            return await _repository.GetOrderItemsByUserIdAsync(id);
        }
    }
}