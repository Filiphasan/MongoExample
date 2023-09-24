using MongoExample.API.Middlewares;

namespace MongoExample.API.Extensions;

public static class MiddlewareExtension
{
    public static void RegisterMiddlewares(this IApplicationBuilder app)
    {
        if (app == null)
        {
            throw new ArgumentNullException(nameof(app));
        }
        
        app.UseMiddleware<ExceptionMiddleware>();
    }
}