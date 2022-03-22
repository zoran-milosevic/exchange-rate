using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Polly;
using ExchangeRate.Interface.Infrastructure;

namespace ExchangeRate.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependenciesFor_Infrastructure(this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        services
                .AddTransient<IHttpClientHelper, HttpClientHelper>()
                .AddHttpClient("PollyWaitAndRetry")
                .AddTransientHttpErrorPolicy(policyBuilder =>
                    policyBuilder.WaitAndRetryAsync(3, retryNumber => TimeSpan.FromMilliseconds(600))
                );
        
        return services;    
    }
}