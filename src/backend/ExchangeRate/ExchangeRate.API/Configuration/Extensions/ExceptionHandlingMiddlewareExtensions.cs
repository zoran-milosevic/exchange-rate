using ExchangeRate.API.Configuration.Middlewares;

namespace ExchangeRate.API.Configuration.Extensions;

public static class ExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseHttpExceptionHandlingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<HttpExceptionHandlingMiddleware>();
    }
}