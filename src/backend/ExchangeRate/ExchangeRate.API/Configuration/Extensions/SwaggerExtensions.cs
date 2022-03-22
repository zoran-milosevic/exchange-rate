using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Mvc.Versioning;
using Swashbuckle.AspNetCore.SwaggerGen;
using ExchangeRate.API.Configuration.Swagger;

namespace ExchangeRate.API.Configuration.Extensions;

public static class SwaggerExtensions
{
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {   
        return services
            .AddApiVersion()
            .AddTransient<IConfigureOptions<SwaggerGenOptions>, CustomSwaggerOptions>()
            .AddSwaggerGen(options => options.OperationFilter<SwaggerDefaultValues>());
    }

    public static IApplicationBuilder UseCustomSwaggerMiddleware(this IApplicationBuilder app, IApiVersionDescriptionProvider provider, ConfigurationManager configuration)
    {
        return app
            .UseSwagger(options =>
            {
                options.RouteTemplate = "/swagger/{documentName}/swagger.json";
            })
            .UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                {
                    options.SwaggerEndpoint($"{description.GroupName}/swagger.json", description.ApiVersion.ToString());
                    options.RoutePrefix = "swagger";
                }
            });
    }

    private static IServiceCollection AddApiVersion(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.UseApiBehavior = false;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}