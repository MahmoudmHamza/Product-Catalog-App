using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProductsCatalogApp.Repositories
{
    public class PostgresRepository<T> : IPostgresRepository<T> where T : class 
    {
        private readonly CatalogDbContext _context;

        public PostgresRepository(CatalogDbContext context)
        {
            _context = context;
        }

        public PostgresRepository()
        {
            
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            try
            {
                return await _context.Set<T>().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve all data for entity {typeof(T)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<T> Get(int id)
        {
            try
            {
                return await _context.Set<T>().FindAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't retrieve data with id: {id} for entity {typeof(T)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<T> Add(T entity)
        {
            try
            {
                await _context.Set<T>().AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't create new entry for entity {typeof(T)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<T> Update(int id, T entity)
        {
            try
            {
                T fetchedEntity = await _context.Set<T>().FindAsync(id);
                if (fetchedEntity == null)
                {
                    return null;
                }
                
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't update entry for entity {typeof(T)} \n ErrorDetails: {ex.Message}");
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                T entity = await _context.Set<T>().FindAsync(id);
                if (entity == null)
                {
                    return false;
                }
                
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Database error: Couldn't delete entry for entity {typeof(T)} \n ErrorDetails: {ex.Message}");
            }
        }
    }
}