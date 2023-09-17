using MongoDB.Driver;
using MongoExample.Data.Context;

namespace MongoExample.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterMyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString("MongoDb")));
        services.AddScoped<MongoDbContext>();

        return services;
    }
}