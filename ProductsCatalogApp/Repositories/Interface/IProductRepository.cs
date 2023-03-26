using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;

namespace ProductsCatalogApp.Repositories
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsByCategoryId(int categoryId);
        Task<Product> AddProductRating(Product product, ProductRating rating);
        Task<IEnumerable<Product>> SearchProducts(string query);
        Task<IEnumerable<Product>> GetProductsByCategoryIdsAsync(List<int> categoryIds);
    }
}