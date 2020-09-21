using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CrossCutting.Persistance
{
    public interface IReadOnlyRepository<T> where T : class
    {
        /// <summary>
        ///  The count.
        /// </summary>
        /// <returns>
        ///The <see cref="long" />.
        /// </returns>
        long Count();

        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="filter">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        long Count(Expression<Func<T, bool>> filter);

        /// <summary>
        ///     The count async.
        /// </summary>
        /// <returns>
        ///     The <see cref="Task" />.
        /// </returns>
        Task<int> CountAsync();

        /// <summary>
        /// The count async.
        /// </summary>
        /// <param name="filter">
        /// The where expression.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<int> CountAsync(Expression<Func<T, bool>> filter);

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Exists(T entity);

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="entity">
        /// The entity.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> ExistsAsync(T entity);

        /// <summary>
        /// The exists.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        bool Exists(object id);

        /// <summary>
        /// The exists async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<bool> ExistsAsync(object id);

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetById(object id);

        /// <summary>
        /// The get by id async.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        Task<T> GetByIdAsync(object id);

        bool Any(Expression<Func<T, bool>> filter = null);
        Task<bool> AnyAsync(Expression<Func<T, bool>> filter = null);

        /// <summary>
        /// get first Or default with filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="orderBy"></param>
        /// <returns></returns>
        T GetFirst(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        TResult GetFirstBySelector<TResult>(Func<T, TResult> selector, Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool tracking = true,
           params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// get first Or default with filter async
        /// </summary>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> order by expression</param>
        /// <returns></returns>
        Task<T> GetFirstAsync(Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
             params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// get Top
        /// </summary>
        /// <param name="length"></param>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <returns></returns>
        IEnumerable<T> GetTop(int length,
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// get Top async
        /// </summary>
        /// <param name="length"></param>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <returns></returns>
        Task<IEnumerable<T>> GetTopAsync(int length,
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null);

        /// <summary>
        /// Get
        /// </summary>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        IEnumerable<T> Get(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Get Async
        /// </summary>
        /// <param name="filter"> expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        Task<List<T>> GetAsync(
         Expression<Func<T, bool>> filter = null,
         Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
         params Expression<Func<T, object>>[] includeProperties);


        IEnumerable<TResult> GetBySelector<TResult>(
            Func<T, TResult> selector,
      Expression<Func<T, bool>> filter = null,
      Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
      params Expression<Func<T, object>>[] includeProperties);


        /// <summary>
        /// Get By filters
        /// </summary>
        /// <param name="filter">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        IEnumerable<T> GetByFilters(
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Get By filters async
        /// </summary>
        /// <param name="filter">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        Task<IEnumerable<T>> GetByFiltersAsync(
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Get Query
        /// </summary>
        /// <param name="filter">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        IQueryable<T> GetQuery(
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// Get Query By filters
        /// </summary>
        /// <param name="filters">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        IQueryable<T> GetQueryByFilters(
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);

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
        IEnumerable<T> GetPaged(int pageIndex, int pageSize, out int totalCount,
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);

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
        Task<(IEnumerable<T> items, int totalCount)> GetPagedAsync(int pageIndex, int pageSize,
        Expression<Func<T, bool>> filter = null,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);

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
        IEnumerable<T> GetPagedByFilters(int pageIndex, int pageSize, out int totalCount,
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);

        /// <summary>
        /// GetPagedByFiltersAsync
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="filters">List of expression filter</param>
        /// <param name="orderBy"> oreder by expression</param>
        /// <param name="includeProperties"> list of Includes properties</param>
        /// <returns></returns>
        Task<(IEnumerable<T> items, int totalCount)> GetPagedByFiltersAsync(int pageIndex, int pageSize,
        IEnumerable<Expression<Func<T, bool>>> filters,
        Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        params Expression<Func<T, object>>[] includeProperties);
    }
}
