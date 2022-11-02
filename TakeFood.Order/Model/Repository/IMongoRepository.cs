using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Order.Model.Entities;
using Order.Utilities;
using System.Linq.Expressions;
using TakeFood.OrderService.Model.Content;
using static Order.Model.Repository.IRepository;

namespace Order.Model.Repository;

/// <summary>
/// I Mongo repository
/// MongoRepository for mongodb
/// </summary>
public interface IMongoRepository<T> : IRepository<T>
        where T : ModelMongoDB
{
    /// <summary>
    /// the collection
    /// </summary>
    IMongoCollection<T> Collection { get; }

    /// <summary>
    /// The dbSet
    /// </summary>
    IMongoQueryable<T> Elements { get; }

    /// <summary>
    /// Get reference of an entity
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    MongoDBRef GetRef(T item);

    /// <summary>
    /// Find async
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="options"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    Task<IList<T>> FindAsync(FilterDefinition<T> filter, FindOptions options = null, int? start = null, int? limit = null);

    /// <summary>
    /// Find và sắp xếp theo id
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    Task<IList<T>> FindSortIdAsync(FilterDefinition<T> filter, int? start = null, int? limit = null);

    /// <summary>
    /// Find async
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    Task<IList<T>> FindAsync(FilterDefinition<T> filter, FindOptions<T> options);

    /// <summary>
    /// Find và sắp xếp theo yêu cầu
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    Task<IList<T>> FindSortAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int? start = null, int? limit = null);

    /// <summary>
    /// Find async with include delete or not
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="includeIsDeleted"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    Task<IList<T>> FindAsync(FilterDefinition<T> filter, bool includeIsDeleted, FindOptions options = null);

    /// <summary>
    /// GetAllRecord
    /// </summary>
    /// <returns></returns>
    Task<IList<T>> GetAllAsync();

    /// <summary>
    /// Get paging data
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    Task<PagingData<T>> GetPagingDataAsync(FilterDefinition<T> filter, FindOptions options = null, int? start = null, int? limit = null);

    /// <summary>
    /// GetPaging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="rowsCount"></param>
    /// <param name="includeIsDeleted">Có tìm field đã xóa không</param>
    /// <param name="sortColumn"></param>
    /// <param name="sortType"></param>
    /// <param name="partitionKey"></param>
    /// <returns></returns>
    Task<PagingData<T>> GetPagingAsync(FilterDefinition<T> filter, int pageNumber, int pageSize, string sortColumn = "", string sortType = CommonConstants.SortTypeASC, bool includeIsDeleted = false);

    /// <summary>
    /// Get paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    Task<PagingData<T>> GetPagingAsync(FilterDefinition<T> filter, int pageNumber, int pageSize, SortDefinition<T> sort, bool includeIsDeleted = false);

    /// <summary>
    /// Delete 1 item async
    /// </summary>
    /// <param name="id"></param>
    /// <param name="deleteBy">Delete by</param>
    /// <returns></returns>
    Task<bool> DeleteAsync(string id, BaseUpdateUserModel deleteBy);

    /// <summary>
    /// Delete many items async
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="deleteBy"></param>
    /// <returns></returns>
    Task<bool> DeleteManyAsync(Expression<Func<T, bool>> filter, BaseUpdateUserModel deleteBy);

    /// <summary>
    /// Delete entity async
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="deleteBy"></param>
    /// <returns></returns>
    Task<bool> DeleteAsync(T entity, BaseUpdateUserModel deleteBy);

    /// <summary>
    /// Insert an entity with session
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    Task<T> InsertWithSessionAsync(T entity, IClientSessionHandle session);

    /// <summary>
    /// UpdateWithSessionAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    Task<T> UpdateWithSessionAsync(T entity, IClientSessionHandle session);

    /// <summary>
    /// UpdateManyWithSessionAsync
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    Task<IList<T>> UpdateManyWithSessionAsync(IList<T> entities, IClientSessionHandle session);

    /// <summary>
    /// Update many
    /// </summary>
    /// <param name="filter">Condition update</param>
    /// <param name="update">Update</param>
    /// <param name="options"></param>
    /// <returns></returns>
    Task<int> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null);

    /// <summary>
    /// DeleteWithSessionAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="deleteBy"></param>
    /// <param name="clientSessionHandle"></param>
    /// <returns></returns>
    Task<bool> DeleteWithSessionAsync(T entity, BaseUpdateUserModel deleteBy, IClientSessionHandle clientSessionHandle);

    /// <summary>
    /// Delete 1 item async
    /// </summary>
    /// <param name="id"></param>
    /// <param name="deleteBy">Delete by</param>
    /// <param name="session">session</param>
    /// <returns></returns>
    Task<bool> DeleteWithSessionAsync(string id, BaseUpdateUserModel deleteBy, IClientSessionHandle session);

    /// <summary>
    /// GetWithSession
    /// </summary>
    /// <param name="id"></param>
    /// <param name="session">session</param>
    /// <returns></returns>
    Task<T> FindByIdWithSessionAsync(string id, IClientSessionHandle session);

    /// <summary>
    /// FindWithSession
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="session">session</param>
    /// <returns></returns>
    Task<IList<T>> FindWithSessionAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session);

    /// <summary>
    /// Save list of items
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    Task<IList<T>> InsertManyWithSessionAsync(IList<T> entities, IClientSessionHandle session);

    /// <summary>
    /// Delete many items
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="deleteBy"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    Task<bool> DeleteManyWithSessionAsync(Expression<Func<T, bool>> filter, BaseUpdateUserModel deleteBy, IClientSessionHandle session);

    /// <summary>
    /// Remove by id async
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    Task<bool> RemoveAsync(string entityId);

    /// <summary>
    /// Remove many items async
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<bool> RemoveManyAsync(Expression<Func<T, bool>> filter);

    /// <summary>
    /// Delete many items
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    Task<bool> RemoveManyWithSessionAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session);

    /// <summary>
    /// Get paging data
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    /*Task<PagingData<T>> GetPagingDataAsync(FilterDefinition<T> filter, FindOptions options = null, int? start = null, int? limit = null);*/

    /// <summary>
    /// GetPaging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="rowsCount"></param>
    /// <param name="includeIsDeleted">Có tìm field đã xóa không</param>
    /// <param name="sortColumn"></param>
    /// <param name="sortType"></param>
    /// <param name="partitionKey"></param>
    /// <returns></returns>
    /*   Task<PagingData<T>> GetPagingAsync(FilterDefinition<T> filter, int pageNumber, int pageSize, string sortColumn = "", string sortType = CommonConstants.SortTypeASC, bool includeIsDeleted = false);*/

    /// <summary>
    /// Get paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    /*Task<PagingData<T>> GetPagingAsync(FilterDefinition<T> filter, int pageNumber, int pageSize, SortDefinition<T> sort, bool includeIsDeleted = false);*/

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update);

    /// <summary>
    /// Find
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="options"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    Task<int> CountAsync(FilterDefinition<T> filter, CountOptions options = null);

    /// <summary>
    /// Find
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<T> FindOneAsync(Expression<Func<T, bool>> filter);


    /// <summary>
    /// Update multiple document in one round to db using bulk write mongodb
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    Task<bool> BulkUpdateAsync(Dictionary<string, UpdateDefinition<T>> updateDefinitions);

    /// <summary>
    /// Update write async
    /// </summary>
    /// <param name="writeModels"></param>
    /// <returns></returns>
    Task<bool> BulkWriteAsync(IList<WriteModel<T>> writeModels, BulkWriteOptions options = null);

    /// <summary>
    /// FindOneAndUpdateAsync
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="update"></param>
    /// <param name="options"></param>
    /// <param name="clientSession"></param>
    /// <returns></returns>
    Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options, IClientSessionHandle clientSession);

    /// <summary>
    /// Get list only columns
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    Task<IList<T>> GetAllAsync(string[] columns = null);

    /// <summary>
    /// Get list only columns
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    Task<IList<T>> GetAllAsync(bool includeIsDeleted);

    /// <summary>
    /// Get all which columns async
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    IList<T> GetAll(string[] columns = null);

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> updateOptions);

    /// <summary>
    /// Find and get specific field
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="filter"></param>
    /// <param name="fieldExpression"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    Task<List<TValue>> FindSpecificFieldAsync<TEntity, TValue>(FilterDefinition<T> filter, Expression<Func<T, TValue>> fieldExpression, SortDefinition<T> sort, int? start = null, int? limit = null, bool isDelete = false) where TEntity : T;
}
