using Microsoft.Extensions.Configuration;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Driver;
using MongoExample.Core.Entities;

namespace MongoExample.Data.Context;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IMongoClient mongoClient, IConfiguration configuration)
    {
        _database = mongoClient.GetDatabase(configuration["MongoDbSettings:DatabaseName"]);

        ConfigureEntity();
    }

    private static void ConfigureEntity()
    {
        // Set All Element Name CamelCase
        // var conventionPack = new ConventionPack { new CamelCaseElementNameConvention() };
        // ConventionRegistry.Register("camelCase", conventionPack, t => true);

        BsonClassMap.RegisterClassMap<Product>(cm =>
        {
            cm.AutoMap();
            cm.MapIdProperty(x => x.Id)
                .SetIdGenerator(StringObjectIdGenerator.Instance);
            cm.MapMember(x => x.Id)
                .SetElementName("id");
            cm.MapMember(x => x.Name)
                .SetElementName("name");
            cm.MapMember(x => x.Price)
                .SetElementName("price");
            cm.MapMember(x => x.Quantity)
                .SetElementName("quantity");
        });
    }

    //Database
    public IMongoDatabase Database => _database;

    //Collections
    public IMongoCollection<Product> Products => _database.GetCollection<Product>(nameof(Product));

    public IMongoCollection<TEntity> GetCollection<TEntity>(MongoCollectionSettings settings = null)
    {
        return _database.GetCollection<TEntity>(typeof(TEntity).Name, settings);
    }
}