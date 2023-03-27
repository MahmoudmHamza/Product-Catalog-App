using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;
using ProductsCatalogApp.Repositories;
using System.Linq;

namespace ProductsCatalogApp.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<User> GetAsync(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<List<User>> GetAllAsync()
        {
            var users = await _repository.GetAll();
            return users.ToList();
        }

        public async Task<User> CreateAsync(User user)
        {
            return await _repository.Add(user);
        }

        public async Task<User> UpdateAsync(int id, User user)
        {
            return await _repository.Update(id, user);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<List<Product>> GetRecommendedProducts(int id)
        {
            return await _repository.GetRecommendedProductsAsync(id);
        }
    }
}