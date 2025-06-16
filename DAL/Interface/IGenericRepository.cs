using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interface
{
    public interface IGenericRepository <T> where T : class
    {
        //Task<IEnumerable<T>> GetAllAsync();
        //IQueryable<T> Entities { get; }
        //Task<T?> GetByIdAsync(object id);
        //Task<PaginatedList<T>> GetPagingAsync(IQueryable<T> query, int pageIndex, int pageSize);
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsyncById(object id);
        Task DeleteAsync(params object[] keyValues);

        Task<List<T>> GetAllByPropertyAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        Task<T> GetByPropertyAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true, string? includeProperties = null);

    }
}
