using SAS.EventsService.API.DependencyInjection;
using SAS.EventsService.Application.DependencyInjection;
using SAS.EventsService.Infrastructure.Persistence.DependencyInjection;
using SAS.EventsService.Infrastructure.Services.DependencyInjection;
using SAS.EventsService.Infrastructure.SignalR;
using SAS.EventsService.Presentation.DependencyInjection;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(new ConfigurationBuilder()
        .AddJsonFile("appsettings.json")
        .Build())
    .Enrich.FromLogContext()
    .WriteTo.Console()
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
try
{
    Log.Information("Starting SAS.EventsService API");

    var builder = WebApplication.CreateBuilder(args);

    builder.Configuration
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddEnvironmentVariables()
        .AddUserSecrets<Program>();


    // Access configuration
    var configuration = builder.Configuration;



    // Add services to the container.

    // adding dependency injection 
    builder.Services
        .AddAPI(configuration)
        .AddApplication()
        .AddPresentation()
        .AddInfrastructureSevices(configuration)
        .AddPersistence(configuration);
                
    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // for secret key storage 
    builder.Configuration.AddUserSecrets<Program>();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    //app.UseCors("AllowFrontendDev");
    app.UseCors("AllowAll");
    app.UseAuthorization();
    app.UseOutputCache();

    app.MapHub<NotificationHub>("/hubs/notifications");
    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}
