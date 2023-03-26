using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductsCatalogApp.Repositories
{
    public interface IPostgresRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Add(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
    }
}