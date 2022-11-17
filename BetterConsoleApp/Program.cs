#region MIT License

// ==========================================================
// 
// ConsoleWithLogging project - Copyright (c) 2022 JeePeeTee
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
// 
// ===========================================================

#endregion

#region usings

using BetterConsoleApp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

#endregion

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
    .ConfigureServices((contect, services) => { services.AddTransient<IGreetingService, GreetingService>(); })
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