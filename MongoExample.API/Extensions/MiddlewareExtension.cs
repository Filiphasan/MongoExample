using MongoExample.API.Middlewares;

namespace MongoExample.API.Extensions;

public static class MiddlewareExtension
{
    public static void RegisterMiddlewares(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<ExceptionMiddleware>();
    }
}