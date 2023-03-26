using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;
using ProductsCatalogApp.Repositories;
using System.Linq;

namespace ProductsCatalogApp.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IPostgresRepository<Category> _repository;

        public CategoryService(IPostgresRepository<Category> repository)
        {
            _repository = repository;
        }
        
        public async Task<Category> GetAsync(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<List<Category>> GetAllAsync()
        {
            var categories = await _repository.GetAll();
            return categories.ToList();
        }

        public async Task<Category> CreateAsync(Category category)
        {
            return await _repository.Add(category);
        }

        public async Task<Category> UpdateAsync(int id, Category category)
        {
            return await _repository.Update(id, category);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.Delete(id);
        }
    }
}