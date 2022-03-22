using System.Net;
using ExchangeRate.Model.HttpException;

namespace ExchangeRate.API.Configuration.Middlewares;

public class HttpExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _env;
    private readonly ILogger<HttpExceptionHandlingMiddleware> _logger;

    public HttpExceptionHandlingMiddleware(
        RequestDelegate next,
        IWebHostEnvironment env,
        ILogger<HttpExceptionHandlingMiddleware> logger
    )
    {
        _next = next;
        _env = env;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
             await _next(context);
        }
        catch (System.Exception exception)
        {
            _logger.LogInformation($"LOGGING::REQUEST {context.Request.Method} {context.Request.Path.Value} => {context.Response.StatusCode}");
            _logger.LogError($"LOGGING::EXCEPTION {exception.Message}");

            if (_env.IsDevelopment())
            {
                var isCustomHttpException = await HandleGlobalErrorExceptionAsync(context, exception);

                if (isCustomHttpException)
                {
                    await HandleCustomHttpErrorExceptionAsync(context, exception);
                }
            }
            else
            {
                await HandleCustomHttpErrorExceptionAsync(context, exception);
            }
        }
    }

    private async Task HandleCustomHttpErrorExceptionAsync(HttpContext context, Exception exception)
    {
        var exceptionResponse = new ExceptionResponse();

        if (exception is CustomHttpException httpException)
        {
            exceptionResponse = new ExceptionResponse
            {
                StatusCode = httpException.StatusCode,
                Message = httpException.Message
            };
        }

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int) HttpStatusCode.OK;
        
        await context.Response.WriteAsync(exceptionResponse.ToJsonString());
    }

    private async Task<bool> HandleGlobalErrorExceptionAsync(HttpContext context, Exception exception)
    {
        var exceptionResponse = new UnintentionalExceptionResponse();
        var isCustomHttpException = true;

        if (exception is not CustomHttpException httpException)
        {
            isCustomHttpException = false;

            exceptionResponse = new UnintentionalExceptionResponse
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Message = exception.Message,
                InnerMessage = exception.InnerException?.Message
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int) System.Net.HttpStatusCode.OK;

            await context.Response.WriteAsync(exceptionResponse.ToJsonString());
        }

        return isCustomHttpException;
    }
}