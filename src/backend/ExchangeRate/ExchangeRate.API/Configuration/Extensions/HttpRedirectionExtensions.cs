namespace ExchangeRate.API.Configuration.Extensions;

public static class HttpRedirectionExtensions
{
    public static IServiceCollection ConfigureHttpsRedirection(this IServiceCollection services, ConfigurationManager configuration)
    {
        return services.AddHttpsRedirection(options =>
        {
            options.RedirectStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status308PermanentRedirect;
            options.HttpsPort = Convert.ToInt16(configuration["https_port"]);
        });
    }
}
