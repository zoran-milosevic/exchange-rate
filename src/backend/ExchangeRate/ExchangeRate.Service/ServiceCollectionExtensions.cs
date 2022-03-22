using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ExchangeRate.Interface.Service.DomainService;
using ExchangeRate.Interface.Service.UseCase;
using ExchangeRate.Infrastructure;
using ExchangeRate.Service.UseCase;
using ExchangeRate.Service.DomainService;

namespace ExchangeRate.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependenciesFor_Service(this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        return services
                .AddDependenciesFor_Infrastructure(configuration)
                .AddScoped<IExchangeRateService, ExchangeRateService>()
                .AddScoped<IExchangeRateManager, ExchangeRateManager>();
    }
}