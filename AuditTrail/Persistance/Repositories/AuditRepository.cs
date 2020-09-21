using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace AuditTrailComponent.Persistance.Repositories
{
    public class AuditRepository<T> : IAuditRepository<T> where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuditRepository{T}"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public AuditRepository(DbContext context)
        {
            Context = context;
            DbSet = context.Set<T>();
        }

        /// <summary>
        /// Gets the context.
        /// </summary>
        protected DbContext Context { get; }

        /// <summary>
        /// Gets the db set.
        /// </summary>
        protected DbSet<T> DbSet { get; }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Add(T entity)
        {
            var addedEntity = DbSet.Add(entity);

            if (Context.Entry(addedEntity.Entity).State == EntityState.Added)
            {
                return addedEntity.Entity;
            }

            return null;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public void Add(IEnumerable<T> entities)
        {
            DbSet.AddRange(entities);
        }

        /// <summary>
        /// The add async.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<T> AddAsync(T entity)
        {
            var addedEntity = await DbSet.AddAsync(entity);

            return addedEntity.Entity;
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public async Task AddAsync(IEnumerable<T> entities)
        {
            await DbSet.AddRangeAsync(entities);
        }

        /// <summary>
        /// Update by Specific Object 
        /// </summary>
        /// <param name="id">Key</param>
        /// <param name="t"> updated Object</param>
        public virtual void Update(object id, T entity)
        {
            var obj = GetById(id);
            Context.Entry(obj).CurrentValues.SetValues(entity);
        }

        /// <summary>
        /// Updated
        /// </summary>
        /// <param name="entityToUpdate"> Updated Object</param>
        public virtual void Update(T entityToUpdate)
        {
            DbSet.Attach(entityToUpdate);
            Context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Delete(T entity)
        {
            var deletedEntity = DbSet.Remove(entity);
            return Context.Entry(deletedEntity.Entity).State == EntityState.Deleted;
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="entities">
        /// The entities.
        /// </param>
        public virtual void Delete(IEnumerable<T> entities)
        {
            DbSet.RemoveRange(entities);
        }

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="t">
        /// The t.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual void DeleteAsync(T entity)
        {
            Context.Entry(entity).State = EntityState.Deleted;
        }

        /// <summary>
        /// The delete by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool DeleteById(object id)
        {
            var entity = GetById(id);
            return Delete(entity);
        }

        /// <summary>
        ///  The count.
        /// </summary>
        /// <returns>
        ///The <see cref="long" />.
        /// </returns>
        public virtual long Count()
        {
            return DbSet.Count();
        }

        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="filter">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public virtual long Count(Expression<Func<T, bool>> filter)
        {
            return DbSet.Count(filter);
        }

        /// <summary>
        ///     The count async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        public virtual async Task<int> CountAsync()
        {
            return await DbSet.CountAsync();
        }

        /// <summary>
        /// The count async.
        /// </summary>
        /// <param name="filter">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<int> CountAsync(Expression<Func<T, bool>> filter)
        {
            return await DbSet.CountAsync(filter);
        }

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Exists(T entity)
        {
            return DbSet.Any(e => e == entity);
        }

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<bool> ExistsAsync(T entity)
        {
            return await DbSet.AnyAsync(e => e == entity);
        }

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public virtual bool Exists(object id)
        {
            return DbSet.Find(id) != null;
        }

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public virtual async Task<bool> ExistsAsync(object id)
        {
            return await DbSet.FindAsync(id) != null;
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public virtual T GetById(object id)
        {
            return DbSet.Find(id);
        }

        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        public async Task<T> GetByIdAsync(object id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual bool Any(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return query.Any();
        }

        public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            return await query.AnyAsync();
        }

        /// <summary>
        /// get first Or default with filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        public virtual T GetFirst(Expression<Func<T, bool>> filter = null,
       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.FirstOrDefault();
        }

        public virtual TResult GetFirstBySelector<TResult>(Func<T, TResult> selector, Expression<Func<T, bool>> filter = null,
       Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query.Select(selector).FirstOrDefault();
        }

        /// <summary>
        /// get first Or default with filter async
        /// </summary>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> order by expression</param>
        /// <returns></returns>
        public virtual async Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.FirstOrDefaultAsync();
        }

        /// <summary>
        /// get Top
        /// </summary>
        /// <param name="length"></param>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetTop(int length, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return orderBy(query).Take(length).ToList();
            }

            return query.Take(length).ToList();
        }

        /// <summary>
        /// get Top async
        /// </summary>
        /// <param name="length"></param>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <returns></returns>
        public async virtual Task<IEnumerable<T>> GetTopAsync(int length, Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Take(length).ToListAsync();
            }

            return await query.Take(length).ToListAsync();
        }

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public virtual IEnumerable<TResult> GetBySelector<TResult>(
            Func<T, TResult> selector,
      Expression<Func<T, bool>> filter = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return orderBy(query).Select(selector);
            }

            return query.Select(selector);

        }


        /// <summary>
        /// Get Async
        /// </summary>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        public virtual Task<List<T>> GetAsync(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToListAsync();
            }

            return query.ToListAsync();
        }

        /// <summary>
        /// Get By filters
        /// </summary>
        /// <param name="filter">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetByFilters(
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        /// <summary>
        /// Get By filters async
        /// </summary>
        /// <param name="filter">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        public virtual async Task<IEnumerable<T>> GetByFiltersAsync(
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        /// <summary>
        /// Get Query
        /// </summary>
        /// <param name="filter">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetQuery(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {

            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        /// <summary>
        /// Get Query By filters
        /// </summary>
        /// <param name="filters">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        public virtual IQueryable<T> GetQueryByFilters(
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return orderBy(query);
            }

            return query;
        }

        /// <summary>
        /// Get Paged result
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetPaged(int pageIndex, int pageSize, out int totalCount,
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            totalCount = query.Select(a => "1").Count();

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }

            return query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// Get paged reslut with option include, filter and order async
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="filter">expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        public virtual async Task<(IEnumerable<T> items, int totalCount)> GetPagedAsync(int pageIndex, int pageSize,
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filter != null)
            {
                query = query.Where(filter);
            }
            var totalCount = query.Select(a => "1").Count();

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return (await orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(), totalCount);
            }

            return (await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(), totalCount);
        }

        /// <summary>
        /// GetPagedByFilters
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <param name="filter">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetPagedByFilters(int pageIndex, int pageSize, out int totalCount,
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }
            totalCount = query.Select(a => "1").Count();

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                return orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToList();
            }

            return query.Skip(pageIndex * pageSize).Take(pageSize).ToList();
        }

        /// <summary>
        /// GetPagedByFiltersAsync
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        public virtual async Task<(IEnumerable<T> items, int totalCount)> GetPagedByFiltersAsync(int pageIndex, int pageSize,
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = DbSet;

            if (filters != null && filters.Count() > 0)
            {
                foreach (var filter in filters)
                {
                    query = query.Where(filter);
                }
            }
            var totalCount = query.Select(a => "1").Count();

            if (includeProperties != null && includeProperties.Any())
            {
                query = query.IncludeMultiple(includeProperties);
            }

            if (orderBy != null)
            {
                if (pageSize > 0)
                    return (await orderBy(query).Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(), totalCount);
                else
                    return (await orderBy(query).ToListAsync(), totalCount);

            }

            if (pageSize > 0)
                return (await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(), totalCount);
            else
                return (await query.ToListAsync(), totalCount);

        }


    }

}
