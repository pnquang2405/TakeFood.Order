using MongoDB.Driver;
using StoreService.Model.Entities;
using StoreService.Model.Repository;

namespace Order.Extension;

public static class ServiceCollectionMongoExtension
{
    /// <summary>
    /// Use Mongo Connection
    /// </summary>
    /// <param name="services"></param>
    /// <param name="uri"></param>
    /// <param name="databaseName"></param>
    public static void AddMongoDb(this IServiceCollection services,
        string uri, string databaseName)
    {
        var client = new MongoClient(uri);
        var database = client.GetDatabase(databaseName);

        services.AddSingleton(database);
    }

    /// <summary>
    /// AddMongo Repository
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="services"></param>
    /// <param name="collectionName"></param>
    public static void AddMongoRepository<TModel>(
            this IServiceCollection services,
            string collectionName)
        where TModel : ModelMongoDB
    {
        services.AddSingleton<IMongoRepository<TModel>>(
            context => new MongoRepository<TModel>(
                context.GetService<IMongoDatabase>()!,
                collectionName));
    }

    /// <summary>
    ///Get mongodb repository
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <param name="services"></param>
    public static IMongoRepository<TModel> GetMongoRepository<TModel>(this IServiceCollection services)
        where TModel : ModelMongoDB
    {
        var sp = services.BuildServiceProvider();
        return sp.GetService<IMongoRepository<TModel>>()!;
    }
}
