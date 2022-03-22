namespace ExchangeRate.API.Configuration.Extensions;

public static class CorsExtensions
{
    public static IServiceCollection ConfigureCors(this IServiceCollection services, IConfiguration configuration)
    {
        var enableCorsOriginsOnly = Convert.ToBoolean(configuration["App:EnableCorsOriginsOnly"]);
        var corsPolicyName = configuration["App:CorsPolicyName"];
        var corsOrigins = configuration["App:CorsOrigins"];
        
        return services.AddCors(options =>
        {
            options.AddPolicy(corsPolicyName, builder =>
            {
                if (enableCorsOriginsOnly)
                {
                    // define origins in appsettings.json 
                    builder.WithOrigins(
                        corsOrigins // App:CorsOrigins in appsettings.json can contain more than one address separated by comma.
                            .Split(",", StringSplitOptions.RemoveEmptyEntries)
                            .Select(o => o.TrimEnd('/'))
                            .ToArray()
                    );
                }
                else
                {
                    builder.AllowAnyOrigin();
                }
                builder
                    .AllowAnyMethod()
                    .AllowAnyHeader();
                    //.AllowCredentials();
            });
        });
    }
}