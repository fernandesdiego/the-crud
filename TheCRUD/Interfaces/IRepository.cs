using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TheCRUD.Interfaces
{
    interface IRepository<T> : IDisposable
    {
        Task AddAsync(T obj);
        Task<T> GetByIdAsync(int id);
        Task<List<T>> GetAllAsync();
        Task UpdateAsync(T obj);
        Task RemoveAsync(int id);
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression);
        Task<int> SaveChanges();
    }
}
