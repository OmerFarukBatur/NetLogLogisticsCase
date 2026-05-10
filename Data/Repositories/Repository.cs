using Core.Entities.Common;
using Core.Helpers;
using Core.Interfaces.IRepositories;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly ApplicationDbContext _context;
        protected readonly DbSet<T> _table;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public DbSet<T> Table => _table;
        public IQueryable<T> GetAll(bool tracking = false)
        {
            var query = _table.AsQueryable();
            return tracking ? query : query.AsNoTracking();
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> predicate, bool tracking = false)
        {
            var query = _table.Where(predicate);
            return tracking ? query : query.AsNoTracking();
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> predicate, bool tracking = false)
        {
            var query = _table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(predicate);
        }

        public async Task<T> GetByIdAsync(int id, bool tracking = false)
        {
            var query = _table.AsQueryable();
            if (!tracking) query = query.AsNoTracking();
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<PaginatedList<T>> GetPagedAsync(
            Expression<Func<T, bool>> filter = null,
            int pageIndex = 1,
            int pageSize = 15,
            bool tracking = false)
        {
            IQueryable<T> query = _table.AsQueryable();
            if (filter != null) query = query.Where(filter);
            if (!tracking) query = query.AsNoTracking();
            return await PaginatedList<T>.CreateAsync(query, pageIndex, pageSize);
        }

        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
        }

        public async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _table.AddRangeAsync(entities);
        }

        public void Remove(T entity)
        {
            if (entity is ISoftDeletable softDeletable)
            {
                softDeletable.IsDeleted = true;
                _table.Update(entity);
            }
            else
            {
                throw new InvalidOperationException(
                    $"{typeof(T).Name} silinemez. Bu entity ISoftDeletable değil.");
            }
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            var list = entities.ToList();
            foreach (var entity in list)
            {
                if (entity is ISoftDeletable softDeletable)
                {
                    softDeletable.IsDeleted = true;
                    _table.Update(entity);
                }
                else
                {
                    throw new InvalidOperationException(
                        $"{typeof(T).Name} silinemez. Bu entity ISoftDeletable değil.");
                }
            }
        }

        public async Task RemoveAsync(int id)
        {
            var entity = await GetByIdAsync(id, tracking: true);
            if (entity == null)
                throw new KeyNotFoundException(
                    $"{typeof(T).Name} bulunamadı. Id: {id}");
            Remove(entity);
        }

        public void Update(T entity)
        {
            _table.Update(entity);
        }

        public async Task<int> SaveAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
