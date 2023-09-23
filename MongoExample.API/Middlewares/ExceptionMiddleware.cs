using System.Net;
using MongoExample.API.Models;

namespace MongoExample.API.Middlewares;

public class ExceptionMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(ILogger<ExceptionMiddleware> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Exception Global");
            await HandleExceptionResponse(context);
        }
    }

    private static async Task HandleExceptionResponse(HttpContext context)
    {
        const int statusCode = (int)HttpStatusCode.InternalServerError;
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = statusCode;

        var response = ResponseModel<object>.SendError(statusCode, "System Error");
        await context.Response.WriteAsJsonAsync(response);
    }
}