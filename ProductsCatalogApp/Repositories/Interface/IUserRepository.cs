using System.Collections.Generic;
using System.Threading.Tasks;
using ProductsCatalogApp.Models;

namespace ProductsCatalogApp.Repositories
{
    public interface IUserRepository : IPostgresRepository<User> 
    {
        Task<List<Product>> GetRecommendedProductsAsync(int id);
    }
}