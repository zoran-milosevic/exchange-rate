using Microsoft.AspNetCore.Mvc.ApiExplorer;
using ExchangeRate.API.Configuration.Extensions;
using ExchangeRate.Model.Mapping;
using ExchangeRate.Services;

public static class ServiceCollectionExtensions
{
    public static void AddEnvironments(this ConfigurationManager configuration,
        IWebHostEnvironment env
    )
    {
        configuration
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
    }

    public static IServiceCollection ConfigureServices(this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        return services
                .ConfigureHsts(configuration)
                .ConfigureHttpsRedirection(configuration)
                .ConfigureCors(configuration)
                .ConfigureSwagger()
                .RegisterDependencies(configuration)
                .AddAutoMapper(typeof(MappingProfile).Assembly);
    }

    public static IApplicationBuilder ConfigurePipeline(this IApplicationBuilder app,
        ConfigurationManager configuration,
        IWebHostEnvironment env
    )
    {
        if (env.IsDevelopment())
        {
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app
                .UseCustomSwaggerMiddleware(provider, configuration);
        }

        return app
                .UseHttpExceptionHandlingMiddleware()
                .UseCors(configuration["App:CorsPolicyName"])
                .UseHsts()
                .UseHttpsRedirection();
    }

    private static IServiceCollection RegisterDependencies(this IServiceCollection services,
        ConfigurationManager configuration
    )
    {
        return services
                .AddDependenciesFor_Service(configuration);
    }
}