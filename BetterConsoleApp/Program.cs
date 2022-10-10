using BetterConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Exceptions;
using Serilog.Formatting.Json;
using Serilog.Sinks.File;

var builder = new ConfigurationBuilder();
BuildConfig(builder);
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Build())
    // Moved to appsettings.json
    //.Enrich.FromLogContext()
    //.Enrich.WithExceptionDetails( )
   
    //.WriteTo.Console(outputTemplate: "[{Timestamp:dd-MM-yyyy HH:mm:ss.fff} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    //.WriteTo.Sink(new FileSink(
    //         @"C:\logs",
    //         new JsonFormatter(renderMessage: true),
    //         null,
    //         null,
    //         false)
    .CreateLogger();

Log.Logger.Information("Application starting...");

var host = Host.CreateDefaultBuilder()
    .ConfigureServices((contect, services) => {
        services.AddTransient<IGreetingService, GreetingService>();
    })
    .UseSerilog()
    .Build();

var svc = ActivatorUtilities.CreateInstance<GreetingService>(host.Services);
svc.Run();

Log.Logger.Information("Application finished...");
Log.CloseAndFlush();

static void BuildConfig(IConfigurationBuilder builder) {
    builder.SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
        .AddEnvironmentVariables();
}