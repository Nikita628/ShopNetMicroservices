using Microsoft.EntityFrameworkCore;
using Order.Application.Contracts.Infrastructure;
using Order.Core.Models;
using System.Linq.Expressions;
using DbModelBase = Order.Infrastructure.Database.Models.ModelBase;

namespace Order.Infrastructure.Database.Repositories
{
    public class Repository<T> : IRepository<T> where T : DbModelBase
    {
        protected readonly OrderContext _dbContext;

        public Repository(OrderContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }        

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).ToListAsync();
        }

        // this type of parameters is too specific
        // we need to accept more abstract filtering parameters
        // e.g. based on string property names
        public async Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            string includeString = null, 
            bool disableTracking = true
            )
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (disableTracking) 
                query = query.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(includeString)) 
                query = query.Include(includeString);

            if (predicate != null) 
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(
            Expression<Func<T, bool>> predicate = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            List<Expression<Func<T, object>>> includes = null, 
            bool disableTracking = true
            )
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (disableTracking) 
                query = query.AsNoTracking();

            if (includes != null) 
                query = includes.Aggregate(query, (current, include) => current.Include(include));

            if (predicate != null) 
                query = query.Where(predicate);

            if (orderBy != null)
                return await orderBy(query).ToListAsync();

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);

            await _dbContext.SaveChangesAsync();

            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;

            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            if (entity != null)
                _dbContext.Set<T>().Remove(entity);

            await _dbContext.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> Search(SearchParamBase searchParam)
        {
            IQueryable<T> query = _dbContext.Set<T>();

            if (searchParam.Id != null)
            {
                query = query.Where(o => o.Id == (int)searchParam.Id.SearchValue);
            }

            return await query.ToListAsync();
        }
    }
}