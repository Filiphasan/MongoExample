using MongoDB.Driver;
using MongoExample.API.Middlewares;
using MongoExample.API.Services.Implementations;
using MongoExample.API.Services.Interfaces;
using MongoExample.Data.Context;

namespace MongoExample.API.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection RegisterMyServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.AddSingleton<IMongoClient>(new MongoClient(configuration.GetConnectionString("MongoDb")));
        services.AddSingleton<MongoDbContext>();

        //Services
        services.AddScoped<IProductService, ProductService>();

        //Middlewares
        services.AddTransient<ExceptionMiddleware>();

        return services;
    }
}