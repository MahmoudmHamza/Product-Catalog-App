using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;
using ProductsCatalogApp.Repositories;
using System.Linq;

namespace ProductsCatalogApp.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> GetAsync(int id)
        {
            return await _repository.Get(id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            var products = await _repository.GetAll();
            return products.ToList();
        }

        public async Task<Product> CreateAsync(Product product)
        {
            return await _repository.Add(product);
        }

        public async Task<Product> UpdateAsync(int id, Product product)
        {
            return await _repository.Update(id, product);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.Delete(id);
        }
        
        public async Task<List<Product>> GetAllProductsByCategoryId(int categoryId)
        {
            return await _repository.GetProductsByCategoryId(categoryId);
        }

        public async Task<Product> AddProductNewRating(Product product, ProductRating rating)
        {
            return await _repository.AddProductRating(product, rating);
        }

        public async Task<List<Product>> SearchProductsByName(string query)
        {
            return await _repository.SearchProducts(query);
        }
    }
}