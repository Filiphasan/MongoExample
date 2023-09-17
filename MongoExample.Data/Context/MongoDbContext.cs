using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Conventions;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoExample.Core.Entities;

namespace MongoExample.Data.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient mongoClient)
    {
        _database = mongoClient.GetDatabase("ECommerce");

        ConfigureEntity();
    }

    private static void ConfigureEntity()
    {
        var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        ConventionRegistry.Register("camelCase", conventionPack, t => true);

        BsonClassMap.RegisterClassMap<Product>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance);
        });
    }

    public IMongoCollection<TEntity> GetCollection<TEntity>()
    {
        return _database.GetCollection<TEntity>(typeof(TEntity).Name);
    }
}