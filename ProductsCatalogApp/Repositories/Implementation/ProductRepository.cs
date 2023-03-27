using System;
using System.Collections.Generic;
using ProductsCatalogApp.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductsCatalogApp.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogDbContext _context;

        public ProductRepository(CatalogDbContext context)
        {
            _context = context;
        }
        
        public async Task<IEnumerable<Product>> GetAll()
        {
            try
            {
                return await _context.Set<Product>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve all data for entity {typeof(Product)} " +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }

        public async Task<Product> Get(int id)
        {
            try
            {
                return await _context.Set<Product>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve data with id: {id} for entity {typeof(Product)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<Product> Add(Product entity)
        {
            try
            {
                await _context.Set<Product>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't create new entry for entity {typeof(Product)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<Product> Update(int id, Product entity)
        {
            try
            {
                var fetchedEntity = await _context.Set<Product>().FindAsync(id);
                if (fetchedEntity == null)
                {
                    return null;
                }
                
                _context.Set<Product>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't update entry for entity {typeof(Product)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var entity = await _context.Set<Product>().FindAsync(id);
                if (entity == null)
                {
                    return false;
                }
                
                _context.Set<Product>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't delete entry for entity {typeof(Product)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            try
            {
                var product = await _context.Set<Product>()
                    .Where(p => p.Category.Id == categoryId).ToListAsync();
                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve all products by the category id {categoryId}" +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }

        public async Task<Product> AddProductRating(Product product, ProductRating rating)
        {
            try
            {
                product.Ratings?.Add(rating);
                await _context.SaveChangesAsync();

                UpdateProductAverageRating(product);

                return product;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't add rating for the given product" +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }

        private async void UpdateProductAverageRating(Product product)
        {
            try
            {
                if (product.Ratings?.Count == 0)
                {
                    product.Rating = 0;
                }
                else
                {
                    product.Rating = (decimal)product.Ratings.Average(r => r.Rating);
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't update average rating for the product with the id {product.Id}" +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }
        
        public async Task<List<Product>> SearchProducts(string query)
        {
            try
            {
                var products = await _context.Products
                    .Where(p => p.Name.Contains(query) || p.Category.Name.Contains(query))
                    .ToListAsync();

                return products;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve results for the given search query" +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryIdsAsync(List<int> categoryIds)
        {
            try
            {
                return await _context.Products
                    .Where(p => categoryIds.Contains(p.Category.Id))
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve all products by category ids for the given search query" +
                                    $"\nErrorDetails: {ex.Message}");
            }
        }
    }
}