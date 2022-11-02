using Order.Model.Entities;
using System.Linq.Expressions;

namespace Order.Model.Repository;

public interface IRepository
{
    /// <summary>
    /// The Interface for repository
    /// </summary>
    public interface IRepository<TEntity> where TEntity : ModelMongoDB
    {
        /// <summary>
        /// Insert async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> InsertAsync(TEntity entity);

        /// <summary>
        /// Insert list of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<IList<TEntity>> InsertManyAsync(IList<TEntity> entities);

        /// <summary>
        /// Update an entity async
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<TEntity> UpdateAsync(TEntity entity);

        /// <summary>
        /// Update list of entities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        Task<IList<TEntity>> UpdateManyAsync(IList<TEntity> entities);

        /// <summary>
        /// Find by Id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="isIncludeDeleted">include deleted result</param>
        /// <returns></returns>
        Task<TEntity> FindByIdAsync(string id, bool isIncludeDeleted = false);

        /// <summary>
        /// Find which filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="limit"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        Task<IList<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, int? start = null, int? limit = null);

        /// <summary>
        /// Find which filter
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="limit"></param>
        /// <param name="start"></param>
        /// <returns></returns>
        IList<TEntity> Find(Expression<Func<TEntity, bool>> filter, int? start = null, int? limit = null);

        /// <summary>
        /// Paging async
        /// </summary>
        /// <param name="filter">Filter</param>
        /// <param name="start">Start</param>
        /// <param name="limit">Limit</param>
        /// <returns></returns>
/*        Task<PagingData<TEntity>> GetPagingDataAsync(Expression<Func<TEntity, bool>> filter = null, int? start = null, int? limit = null);*/

        /// <summary>
        /// Count element in database
        /// </summary>
        /// <param name="filter">Count</param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null);
    }
}
