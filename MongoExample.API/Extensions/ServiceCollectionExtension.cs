using MongoDB.Driver;
using MongoExample.API.Services.Implementations;
using MongoExample.API.Services.Interfaces;
using MongoExample.Data.Context;

namespace MongoExample.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterMyServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString("MongoDb")));
        services.AddScoped<MongoDbContext>();

        services.AddTransient<IProductService, ProductService>();

        return services;
    }
}