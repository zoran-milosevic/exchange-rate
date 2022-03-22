namespace ExchangeRate.API.Configuration.Extensions;

public static class HstsExtensions
{
    public static IServiceCollection ConfigureHsts(this IServiceCollection services, ConfigurationManager configuration)
        {
            return services.AddHsts(options =>
            {
                var hosts = configuration
                    .GetSection("Hsts:excludedHosts")
                    .GetChildren()
                    .ToArray()
                    .Select(c => c.Value)
                    .ToList();

                hosts.ForEach(host => {
                    options.ExcludedHosts.Add(host);
                });
                
                options.Preload = Convert.ToBoolean(configuration["Hsts:preload"]);
                options.IncludeSubDomains = Convert.ToBoolean(configuration["Hsts:includeSubDomains"]);
                options.MaxAge = TimeSpan.FromDays(Convert.ToInt16(configuration["Hsts:maxAgeInDays"]));
            });
        }
}
