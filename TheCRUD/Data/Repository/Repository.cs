using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TheCRUD.Interfaces;

namespace TheCRUD.Data.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ProdContext _context;
        protected readonly DbSet<T> _dbset;

        public Repository(ProdContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }


        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await _dbset.AsNoTracking<T>().Where(expression).ToListAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbset.FindAsync(id);
        }
        public virtual async Task AddAsync(T obj)
        {
            _dbset.Add(obj);
            await SaveChanges();
        }
        public virtual async Task RemoveAsync(int id)
        {            
            _dbset.Remove(await GetByIdAsync(id));
            await SaveChanges();
        }
        public virtual async Task UpdateAsync(T obj)
        {
            _dbset.Update(obj);
            await SaveChanges();
        }
        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
