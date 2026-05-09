using Core.Entities.Common;
using Core.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Core.Interfaces.IRepositories
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
        IQueryable<T> GetAll(bool tracking = false);
        IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = false);
        Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = false);
        Task<T> GetByIdAsync(int id, bool tracking = false);
        Task<PaginatedList<T>> GetPagedAsync(
            Expression<Func<T, bool>> filter = null,
            int pageIndex = 1,
            int pageSize = 25,
            bool tracking = false);

        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        Task RemoveAsync(int id);
        void Update(T entity);
        Task<int> SaveAsync();
    }
}
