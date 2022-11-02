using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Order.Model.Entities;
using Order.Utilities;
using Order.Utilities.Helper;
using System.Linq.Expressions;
using TakeFood.OrderService.Model.Content;

namespace Order.Model.Repository;

/// <summary>
/// Mongodb repository 
/// </summary>
public partial class MongoRepository<T>
    : IMongoRepository<T> where T : ModelMongoDB
{

    /// <summary>
    /// Name of the collection
    /// </summary>
    private readonly string name;

    /// <summary>
    /// Database
    /// </summary>
    private readonly IMongoDatabase database;

    /// <summary>
    /// Set for linq query
    /// </summary>
    public IMongoQueryable<T> Elements => Collection.AsQueryable().Where(element => element.IsDeleted == false);

    /// <summary>
    /// the collection
    /// </summary>
    public IMongoCollection<T> Collection { get; }

    /// <summary>
    /// Initialize with a database set
    /// </summary>
    /// <param name="database"></param>
    /// <param name="collectionName">name of the collection</param>
    public MongoRepository(IMongoDatabase database, string collectionName = null)
    {
        this.database = database;
        name = collectionName ?? typeof(T).Name;
        Collection = database.GetCollection<T>(name);
    }

    /// <summary>
    /// Delete 1 item async
    /// </summary>
    /// <param name="id"></param>
    /// <param name="deleteBy">Delete by</param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(string id, BaseUpdateUserModel deleteBy)
    {
        var updateDefinition = CreateDeleted(deleteBy);
        var updated = await Collection.UpdateOneAsync(e => e.Id.Equals(id), updateDefinition);
        return updated.MatchedCount == 1;
    }

    /// <summary>
    /// Delete many items async
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="deleteBy"></param>
    /// <returns></returns>
    public async Task<bool> DeleteManyAsync(Expression<Func<T, bool>> filter, BaseUpdateUserModel deleteBy)
    {
        var updateDefinition = CreateDeleted(deleteBy);
        var updated = await Collection.UpdateManyAsync(filter, updateDefinition);
        return updated.MatchedCount >= 1;
    }

    /// <summary>
    /// Save list of items async
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task<IList<T>> InsertManyAsync(IList<T> entities)
    {
        var result = new List<T>();

        entities.ToList().ForEach(entity =>
        {
            entity.Id = ObjectId.GenerateNewId().ToString();
            entity.IsDeleted = false;
            result.Add(entity);
        });
        await Collection.InsertManyAsync(result);
        return result;
    }

    /// <summary>
    /// Get a reference of an item
    /// </summary>
    /// <param name="item"></param>
    /// <returns></returns>
    public MongoDBRef GetRef(T item)
    {
        return new MongoDBRef(name, item.Id);
    }

    /// <summary>
    /// Find objet
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<T> FindByIdAsync(string id, bool isIncludeDeleted = false)
    {
        T item;
        if (isIncludeDeleted)
        {
            item = await Collection.AsQueryable().Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        }
        else
        {
            item = await Elements.Where(e => e.Id.Equals(id)).FirstOrDefaultAsync();
        }
        return item;
    }

    /// <summary>
    /// Get all which columns async
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public IList<T> GetAll(string[] columns = null)
    {
        if (columns != null)
        {
            ProjectionDefinition<T> project = null;
            var cursor = Collection.Find(e => e.IsDeleted == false);
            foreach (string columnName in columns)
            {
                if (project == null)
                {
                    project = Builders<T>.Projection.Include(columnName);
                }
                else
                {
                    project = project.Include(columnName);
                }
            }
            if (project != null)
            {
                return cursor.Project<T>(project).ToList();
            }
            else
            {
                return cursor.ToList();
            }
        }
        return Elements.ToList();
    }

    /// <summary>
    /// Get all which columns async
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public async Task<IList<T>> GetAllAsync(string[] columns = null)
    {
        if (columns != null)
        {
            ProjectionDefinition<T> project = null;
            var cursor = Collection.Find(e => e.IsDeleted == false);
            foreach (string columnName in columns)
            {
                if (project == null)
                {
                    project = Builders<T>.Projection.Include(columnName);
                }
                else
                {
                    project = project.Include(columnName);
                }
            }
            if (project != null)
            {
                return await cursor.Project<T>(project).ToListAsync();
            }
            else
            {
                return await cursor.ToListAsync();
            }
        }
        return await Elements.ToListAsync();
    }

    /// <summary>
    /// Get all with include delete or not
    /// </summary>
    /// <param name="columns"></param>
    /// <returns></returns>
    public async Task<IList<T>> GetAllAsync(bool includeIsDeleted)
    {

        if (includeIsDeleted)
        {
            return await Collection.Find(m => true).ToListAsync();
        }
        return await Elements.ToListAsync();
    }

    /// <summary>
    /// Insert
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> InsertAsync(T entity)
    {
        return await SaveAsync(entity);
    }

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> UpdateAsync(T entity)
    {
        if (entity != null && entity.Id != null)
        {
            await Collection.ReplaceOneAsync(item => item.Id == entity.Id, entity);
            return entity;
        }
        return null;
    }

    /// <summary>
    /// Update many async
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task<IList<T>> UpdateManyAsync(IList<T> entities)
    {
        var ele = Utils.SplitList(entities.ToList());
        foreach (var listEntities in ele)
        {
            var tasks = new List<Task>();
            foreach (var item in listEntities)
            {
                tasks.Add(UpdateAsync(item));
            }
            await Task.WhenAll(tasks.ToArray());
        }
        return entities;
    }


    /// <summary>
    /// Update many async
    /// </summary>
    /// <param name="entities"></param>
    /// <returns></returns>
    public async Task<int> UpdateManyAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, UpdateOptions options = null)
    {
        var temp = await Collection.UpdateManyAsync(filter, update, options);
        return (int)temp.MatchedCount;
    }


    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="deleteBy"></param>
    /// <returns></returns>
    public async Task<bool> DeleteAsync(T entity, BaseUpdateUserModel deleteBy)
    {
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.UpdatedDate = deleteBy.UpdatedDate;
            if (await UpdateAsync(entity) != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Find
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<T> FindOneAsync(Expression<Func<T, bool>> filter)
    {
        return await Elements.Where(filter).FirstOrDefaultAsync();
    }

    /// <summary>
    /// Find
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="options"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<int> CountAsync(FilterDefinition<T> filter, CountOptions options = null)
    {
        filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Where(e => e.IsDeleted == false));
        return (int)await Collection.CountDocumentsAsync(filter, options);
    }

    /// <summary>
    /// Find async
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="options"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<IList<T>> FindAsync(FilterDefinition<T> filter, FindOptions options = null, int? start = null, int? limit = null)
    {
        filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Where(e => e.IsDeleted == false));
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            return await Collection.Find(filter, options).Skip(start).Limit(limit).ToListAsync();
        }
        return await Collection.Find(filter, options).ToListAsync();
    }


    /// <summary>
    /// Find sắp xếp theo id
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<IList<T>> FindSortIdAsync(FilterDefinition<T> filter, int? start = null, int? limit = null)
    {
        filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Where(e => e.IsDeleted == false));
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            return await Collection.Find(filter).SortBy(e => e.Id).Skip(start).Limit(limit).ToListAsync();
        }
        return await Collection.Find(filter).SortBy(e => e.Id).ToListAsync();
    }

    /// <summary>
    /// Find async
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<IList<T>> FindAsync(FilterDefinition<T> filter, FindOptions<T> options)
    {
        filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Where(e => e.IsDeleted == false));
        var t = await Collection.FindAsync(filter, options);
        return await t.ToListAsync();
    }

    /// <summary>
    /// Find async with include delete or not
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="includeIsDeleted"></param>
    /// <param name="options"></param>
    /// <returns></returns>
    public async Task<IList<T>> FindAsync(FilterDefinition<T> filter, bool includeIsDeleted, FindOptions options = null)
    {
        if (!includeIsDeleted)
        {
            filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Where(e => e.IsDeleted == false));
        }
        return await Collection.Find(filter, options).ToListAsync();
    }

    /// <summary>
    /// Find và sắp xếp theo yêu cầu
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <param name="sort"></param>
    /// <returns></returns>
    public async Task<IList<T>> FindSortAsync(FilterDefinition<T> filter, SortDefinition<T> sort, int? start = null, int? limit = null)
    {
        filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Where(e => e.IsDeleted == false));
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            return await Collection.Find(filter).Sort(sort).Skip(start).Limit(limit).ToListAsync();
        }
        return await Collection.Find(filter).Sort(sort).ToListAsync();
    }

    /// <summary>
    /// Find filter
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public IList<T> Find(Expression<Func<T, bool>> filter, int? start = null, int? limit = null)
    {
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            return Elements.Where(filter).Skip(start.Value).Take(limit.Value).ToList();
        }
        return Elements.Where(filter).ToList();
    }

    /// <summary>
    /// Find async
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<IList<T>> FindAsync(Expression<Func<T, bool>> filter, int? start = null, int? limit = null)
    {
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            return await Elements.Where(filter).Skip(start.Value).Take(limit.Value).ToListAsync();
        }
        return await Elements.Where(filter).ToListAsync();
    }

    /// <summary>
    /// Count
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<int> CountAsync(Expression<Func<T, bool>> filter = null)
    {
        return await Elements.Where(filter).CountAsync();
    }

    /// <summary>
    /// Get paging data
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    /// TODO
    /*public async Task<PagingData<T>> GetPagingDataAsync(FilterDefinition<T> filter, FindOptions options = null, int? start = null, int? limit = null)
    {
        var count = (int)Collection.Find(filter, options).CountDocuments();
        PagingData<T> rs = new PagingData<T>(count, null);
        filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Where(e => e.IsDeleted == false));
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            rs.Data = await Collection.Find(filter, options).Skip(start).Limit(limit).ToListAsync();
        }
        else
        {
            rs.Data = await Collection.Find(filter, options).ToListAsync();
        }
        return rs;
    }*/


    /// <summary>
    /// Get paging data
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    /*public async Task<PagingData<T>> GetPagingDataAsync(Expression<Func<T, bool>> filter = null, int? start = null, int? limit = null)
    {
        var count = await CountAsync(filter);
        PagingData<T> rs = new PagingData<T>(count, null);
        var query = Elements.Where(e => true);
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            rs.Data = await query.Skip(start.Value).Take(limit.Value).ToListAsync();
        }
        else
        {
            rs.Data = await query.ToListAsync();
        }
        return rs;
    }*/

    /// <summary>
    /// Get an element by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private async Task<T> GetAsync(string id)
    {
        return await Elements.FirstOrDefaultAsync(item => item.Id.Equals(id) && item.IsDeleted == false);
    }

    /// <summary>
    /// Create update definition
    /// </summary>
    /// <param name="deleteBy"></param>
    /// <returns></returns>
    private static UpdateDefinition<T> CreateDeleted(BaseUpdateUserModel deleteBy)
    {
        var dicByModel = new Dictionary<string, string>
            {
                {"userId", deleteBy?.UpdatedBy?.UserId },
                { "displayName", deleteBy?.UpdatedBy?.DisplayName }
            };
        var dictionary = new Dictionary<string, object>
            {
                { "isDeleted", true },
                { "updatedBy", dicByModel },
                { "updatedDate", deleteBy?.UpdatedDate==null? DateTime.Now :deleteBy.UpdatedDate }
            };
        var dic = new Dictionary<string, object>
            {
                { "$set", dictionary }
            };
        return new BsonDocument(dic);
    }

    /// <summary>
    /// Save an item
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    private async Task<T> SaveAsync(T entity)
    {
        var existingEntity = await GetAsync(entity.Id);

        if (existingEntity == null)
        {
            if (entity.Id == null)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.Id = ObjectId.GenerateNewId().ToString();
            }
            entity.IsDeleted = false;
            await Collection.InsertOneAsync(entity);
        }
        else
        {
            entity.UpdatedDate = DateTime.UtcNow;
            await Collection.ReplaceOneAsync(item => item.Id == entity.Id, entity);
        }
        return entity;
    }

    /// <summary>
    /// Save an item With Session
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    private async Task<T> SaveWithSessionAsync(T entity, IClientSessionHandle session)
    {
        var existingEntity = await GetAsync(entity.Id);

        if (existingEntity == null)
        {
            if (entity.Id == null)
            {
                entity.CreatedDate = DateTime.UtcNow;
                entity.Id = ObjectId.GenerateNewId().ToString();
            }
            entity.IsDeleted = false;
            await Collection.InsertOneAsync(session, entity);
        }
        else
        {
            entity.UpdatedDate = DateTime.UtcNow;
            await Collection.ReplaceOneAsync(session, item => item.Id == entity.Id, entity);
        }
        return entity;
    }

    /// <summary>
    /// InsertWithSessionAsync
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="clientSession"></param>
    /// <returns></returns>
    public async Task<T> InsertWithSessionAsync(T entity, IClientSessionHandle clientSession)
    {
        return await SaveWithSessionAsync(entity, clientSession);
    }


    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public async Task<T> UpdateWithSessionAsync(T entity, IClientSessionHandle session)
    {
        if (entity != null && entity.Id != null)
        {
            entity.UpdatedDate = DateTime.UtcNow;
            await Collection.ReplaceOneAsync(session, item => item.Id == entity.Id, entity);
            return entity;
        }
        return null;
    }

    /// <summary>
    /// Delete entity
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="deleteBy"></param>
    /// <param name="clientSessionHandle"></param>
    /// <returns></returns>
    public async Task<bool> DeleteWithSessionAsync(T entity, BaseUpdateUserModel deleteBy, IClientSessionHandle clientSessionHandle)
    {
        if (entity != null)
        {
            entity.IsDeleted = true;
            entity.UpdatedDate = deleteBy?.UpdatedDate;
            if (await UpdateWithSessionAsync(entity, clientSessionHandle) != null)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Delete 1 item async
    /// </summary>
    /// <param name="id"></param>
    /// <param name="deleteBy">Delete by</param>
    /// <param name="session">Delete by</param>
    /// <returns></returns>
    public async Task<bool> DeleteWithSessionAsync(string id, BaseUpdateUserModel deleteBy, IClientSessionHandle session)
    {
        var updateDefinition = CreateDeleted(deleteBy);
        var updated = await Collection.UpdateOneAsync(session, e => e.Id.Equals(id), updateDefinition);
        return updated.MatchedCount == 1;
    }

    /// <summary>
    /// Update many async
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public async Task<IList<T>> UpdateManyWithSessionAsync(IList<T> entities, IClientSessionHandle session)
    {
        IList<T> list = new List<T>();
        foreach (var item in entities)
        {
            var temp = await UpdateWithSessionAsync(item, session);
            if (temp != null)
            {
                list.Add(temp);
            }
        }
        return list;
    }

    /// <summary>
    /// Find By Id WithSession
    /// </summary>
    /// <param name="id"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public async Task<T> FindByIdWithSessionAsync(string id, IClientSessionHandle session)
    {
        return (await Collection.FindAsync(session, item => item.Id.Equals(id) && item.IsDeleted == false)).FirstOrDefault();
    }

    /// <summary>
    /// FindWithSession
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public async Task<IList<T>> FindWithSessionAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session)
    {
        return (await Collection.FindAsync(session, filter)).ToList();
    }

    /// <summary>
    /// Save list of items
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public async Task<IList<T>> InsertManyWithSessionAsync(IList<T> entities, IClientSessionHandle session)
    {
        var result = new List<T>();

        entities.ToList().ForEach(entity =>
        {
            if (entity.Id == null)
            {
                entity.Id = ObjectId.GenerateNewId().ToString();
            }
            entity.IsDeleted = false;
            result.Add(entity);
        });
        await Collection.InsertManyAsync(session, result);
        return result;
    }

    /// <summary>
    /// Delete many items
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="deleteBy"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public async Task<bool> DeleteManyWithSessionAsync(Expression<Func<T, bool>> filter, BaseUpdateUserModel deleteBy, IClientSessionHandle session)
    {
        var updateDefinition = CreateDeleted(deleteBy);
        var updated = await Collection.UpdateManyAsync(session, filter, updateDefinition);
        return updated.MatchedCount >= 1;
    }

    /// <summary>
    /// Remove by id async
    /// </summary>
    /// <param name="entityId"></param>
    /// <returns></returns>
    public async Task<bool> RemoveAsync(string entityId)
    {
        var updated = await Collection.DeleteOneAsync(m => m.Id == entityId);
        return updated.DeletedCount == 1;
    }

    /// <summary>
    /// Delete many items
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public async Task<bool> RemoveManyWithSessionAsync(Expression<Func<T, bool>> filter, IClientSessionHandle session)
    {
        var updated = await Collection.DeleteManyAsync(session, filter);
        return updated.DeletedCount >= 1;
    }

    /// <summary>
    /// Remove many items async
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<bool> RemoveManyAsync(Expression<Func<T, bool>> filter)
    {
        var updated = await Collection.DeleteManyAsync(filter);
        return updated.DeletedCount >= 1;
    }

    /// <summary>
    /// Get paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="rowsCount"></param>
    /// <param name="sortColumn"></param>
    /// <param name="sortType"></param>
    /// <param name="partitionKey"></param>
    /// <returns></returns>
    /*public async Task<PagingData<T>> GetPagingAsync(FilterDefinition<T> filter, int pageNumber, int pageSize, string sortColumn = "", string sortType = CommonConstants.SortTypeASC, bool includeIsDeleted = false)
    {
        IList<T> data;
        if (!includeIsDeleted)
        {
            filter = filter != null ? filter & Builders<T>.Filter.Where(m => !m.IsDeleted) : Builders<T>.Filter.Where(m => !m.IsDeleted);
        }
        if (filter == null)
        {
            filter = Builders<T>.Filter.Where(m => !m.IsDeleted || m.IsDeleted);
        }
        var count = await Collection.CountDocumentsAsync(filter);
        // Handle sort
        SortDefinition<T> sortBuilder = null;
        if (!string.IsNullOrEmpty(sortColumn))
        {
            switch (sortType.ToLower())
            {
                case CommonConstants.SortTypeDESC:
                    sortBuilder = Builders<T>.Sort.Descending(sortColumn).Ascending(e => e.Id);
                    break;
                default:
                    sortBuilder = Builders<T>.Sort.Ascending(sortColumn).Ascending(e => e.Id);
                    break;
            }
        }

        if (sortBuilder != null)
        {
            data = Collection.Find(filter).Sort(sortBuilder).Skip(pageNumber * pageSize).Limit(pageSize).ToList();
        }
        else
        {
            data = Collection.Find(filter).Skip(pageNumber * pageSize).Limit(pageSize).ToList();
        }
        var rs = new PagingData<T>((int)count, data);
        return rs;
    }*/

    /// <summary>
    /// Get paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    /*public async Task<PagingData<T>> GetPagingAsync(FilterDefinition<T> filter, int pageNumber, int pageSize, SortDefinition<T> sort, bool includeIsDeleted = false)
    {
        IList<T> data;
        if (!includeIsDeleted)
        {
            filter = filter != null ? filter & Builders<T>.Filter.Where(m => !m.IsDeleted) : Builders<T>.Filter.Where(m => !m.IsDeleted);
        }
        if (filter == null)
        {
            filter = Builders<T>.Filter.Where(m => !m.IsDeleted || m.IsDeleted);
        }
        var count = await Collection.CountDocumentsAsync(filter);

        if (sort != null)
        {
            data = Collection.Find(filter).Sort(sort).Skip(pageNumber * pageSize).Limit(pageSize).ToList();
        }
        else
        {
            data = Collection.Find(filter).Skip(pageNumber * pageSize).Limit(pageSize).ToList();
        }
        var rs = new PagingData<T>((int)count, data);
        return rs;
    }*/

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update)
    {
        var entity = await Collection.FindOneAndUpdateAsync(filter, update);
        return entity;
    }

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> FindOneAndUpdateAsync(FilterDefinition<T> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> options, IClientSessionHandle clientSession)
    {
        var temp = await Collection.FindOneAndUpdateAsync(clientSession, filter, update, options);
        return temp;
    }


    /// <summary>
    /// Update multiple document in one round to db using bulk write mongodb
    /// </summary>
    /// <param name="entities"></param>
    /// <param name="session"></param>
    /// <returns></returns>
    public async Task<bool> BulkUpdateAsync(Dictionary<string, UpdateDefinition<T>> updateDefinitions)
    {

        IList<T> list = new List<T>();
        var bulkOps = new List<WriteModel<T>>();
        foreach (var item in updateDefinitions)
        {
            var upsertOne = new UpdateOneModel<T>(
                            Builders<T>.Filter.Where(x => x.Id == item.Key),
                            item.Value)
            { IsUpsert = true };
            bulkOps.Add(upsertOne);
        }
        await Collection.BulkWriteAsync(bulkOps);
        return true;
    }


    /// <summary>
    /// Update write async
    /// </summary>
    /// <param name="writeModels"></param>
    /// <returns></returns>
    public async Task<bool> BulkWriteAsync(IList<WriteModel<T>> writeModels, BulkWriteOptions options = null)
    {
        await Collection.BulkWriteAsync(writeModels, options);
        return true;
    }

    /// <summary>
    /// Update entity
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    public async Task<T> FindOneAndUpdateAsync(Expression<Func<T, bool>> filter, UpdateDefinition<T> update, FindOneAndUpdateOptions<T> updateOptions)
    {
        var entity = await Collection.FindOneAndUpdateAsync(filter, update, updateOptions);
        return entity;
    }

    /// <summary>
    /// Tìm và select field
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="filter"></param>
    /// <param name="fieldExpression"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<List<TValue>> FindSpecificFieldAsync<TEntity, TValue>(FilterDefinition<T> filter, Expression<Func<T, TValue>> fieldExpression, SortDefinition<T> sort, int? start = null, int? limit = null, bool includeDelete = false) where TEntity : T
    {
        if (filter == null)
        {
            filter = Builders<T>.Filter.Where(m => includeDelete ? true : !m.IsDeleted);
        }
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            return await Collection
            .Find(filter)
            .Sort(sort)
            .Skip(start)
            .Limit(limit)
            .Project(new ProjectionDefinitionBuilder<T>().Expression(fieldExpression))
            .ToListAsync();
        }
        return await Collection
            .Find(filter)
            .Sort(sort)
            .Project(new ProjectionDefinitionBuilder<T>().Expression(fieldExpression))
            .ToListAsync();
    }

    public async Task<IList<T>> GetAllAsync()
    {
        return await Collection.Find(x => true).ToListAsync();
    }

    /// <summary>
    /// Get paging data
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<PagingData<T>> GetPagingDataAsync(FilterDefinition<T> filter, FindOptions options = null, int? start = null, int? limit = null)
    {
        var count = (int)Collection.Find(filter, options).CountDocuments();
        PagingData<T> rs = new PagingData<T>(count, null);
        filter = Builders<T>.Filter.And(filter, Builders<T>.Filter.Where(e => e.IsDeleted == false));
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            rs.Data = await Collection.Find(filter, options).Skip(start).Limit(limit).ToListAsync();
        }
        else
        {
            rs.Data = await Collection.Find(filter, options).ToListAsync();
        }
        return rs;
    }


    /// <summary>
    /// Get paging data
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="start"></param>
    /// <param name="limit"></param>
    /// <returns></returns>
    public async Task<PagingData<T>> GetPagingDataAsync(Expression<Func<T, bool>> filter = null, int? start = null, int? limit = null)
    {
        var count = await CountAsync(filter);
        PagingData<T> rs = new PagingData<T>(count, null);
        var query = Elements.Where(e => true);
        if (filter != null)
        {
            query = query.Where(filter);
        }
        if (start != null && start.Value > -1 && limit != null && limit.Value > 0)
        {
            rs.Data = await query.Skip(start.Value).Take(limit.Value).ToListAsync();
        }
        else
        {
            rs.Data = await query.ToListAsync();
        }
        return rs;
    }

    /// <summary>
    /// Get paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <param name="rowsCount"></param>
    /// <param name="sortColumn"></param>
    /// <param name="sortType"></param>
    /// <param name="partitionKey"></param>
    /// <returns></returns>
    public async Task<PagingData<T>> GetPagingAsync(FilterDefinition<T> filter, int pageNumber, int pageSize, string sortColumn = "", string sortType = CommonConstants.SortTypeASC, bool includeIsDeleted = false)
    {
        IList<T> data;
        if (!includeIsDeleted)
        {
            filter = filter != null ? filter & Builders<T>.Filter.Where(m => !m.IsDeleted) : Builders<T>.Filter.Where(m => !m.IsDeleted);
        }
        if (filter == null)
        {
            filter = Builders<T>.Filter.Where(m => !m.IsDeleted || m.IsDeleted);
        }
        var count = await Collection.CountDocumentsAsync(filter);
        // Handle sort
        SortDefinition<T> sortBuilder = null;
        if (!string.IsNullOrEmpty(sortColumn))
        {
            switch (sortType.ToLower())
            {
                case CommonConstants.SortTypeDESC:
                    sortBuilder = Builders<T>.Sort.Descending(sortColumn).Ascending(e => e.Id);
                    break;
                default:
                    sortBuilder = Builders<T>.Sort.Ascending(sortColumn).Ascending(e => e.Id);
                    break;
            }
        }

        if (sortBuilder != null)
        {
            data = Collection.Find(filter).Sort(sortBuilder).Skip(pageNumber * pageSize).Limit(pageSize).ToList();
        }
        else
        {
            data = Collection.Find(filter).Skip(pageNumber * pageSize).Limit(pageSize).ToList();
        }
        var rs = new PagingData<T>((int)count, data);
        return rs;
    }

    /// <summary>
    /// Get paging
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="filter"></param>
    /// <param name="pageNumber"></param>
    /// <param name="pageSize"></param>
    /// <returns></returns>
    public async Task<PagingData<T>> GetPagingAsync(FilterDefinition<T> filter, int pageNumber, int pageSize, SortDefinition<T> sort, bool includeIsDeleted = false)
    {
        IList<T> data;
        if (!includeIsDeleted)
        {
            filter = filter != null ? filter & Builders<T>.Filter.Where(m => !m.IsDeleted) : Builders<T>.Filter.Where(m => !m.IsDeleted);
        }
        if (filter == null)
        {
            filter = Builders<T>.Filter.Where(m => !m.IsDeleted || m.IsDeleted);
        }
        var count = await Collection.CountDocumentsAsync(filter);

        if (sort != null)
        {
            data = Collection.Find(filter).Sort(sort).Skip(pageNumber * pageSize).Limit(pageSize).ToList();
        }
        else
        {
            data = Collection.Find(filter).Skip(pageNumber * pageSize).Limit(pageSize).ToList();
        }
        var rs = new PagingData<T>((int)count, data);
        return rs;
    }
}
