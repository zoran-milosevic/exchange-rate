var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironments(builder.Environment);

builder.Services
        .ConfigureServices(builder.Configuration)
        .AddEndpointsApiExplorer()
        .AddRouting(options => {
            options.LowercaseUrls = true;
            options.LowercaseQueryStrings = true;
        })
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.WriteIndented = true;
        });

var app = builder.Build();

app.ConfigurePipeline(builder.Configuration, app.Environment);
app.MapControllers();
app.Run();