using System.Collections.Generic;
using ProductsCatalogApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductsCatalogApp.Repositories
{
    public class ProductRepository : PostgresRepository<Product>, IProductRepository
    {
        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            return await _context.Set<Product>().Where(p => p.Category.Id == categoryId).ToListAsync();
        }

        public async Task<Product> AddProductRating(Product product, ProductRating rating)
        {
            product.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            UpdateProductAverageRating(product);

            return product;
        }

        private async void UpdateProductAverageRating(Product product)
        {
            if (product.Ratings.Count == 0)
            {
                product.Rating = 0;
            }
            else
            {
                product.Rating = (decimal)product.Ratings.Average(r => r.Rating);
            }

            await _context.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<Product>> SearchProducts(string query)
        {
            var products = await _context.Products
                .Where(p => p.Name.Contains(query) || p.Category.Name.Contains(query))
                .ToListAsync();

            return products;
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdsAsync(List<int> categoryIds)
        {
            return await _context.Products
                .Where(p => categoryIds.Contains(p.CategoryId))
                .ToListAsync();
        }
    }
}