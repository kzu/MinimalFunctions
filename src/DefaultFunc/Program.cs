using System.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

var env = Environment.GetEnvironmentVariable("AZURE_FUNCTIONS_ENVIRONMENT") ?? "Development";

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureAppConfiguration(config =>
    {
        config.AddJsonFile(Path.Combine("appsettings.json"), optional: true, reloadOnChange: false)
              .AddDotNetConfig()
              .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
              .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
    })
    .ConfigureServices(s =>
    {
    })
    .ConfigureLogging(l =>
    {
    })
    .Build();

if (bool.TryParse(Environment.GetEnvironmentVariable("ATTACH_DEBUGGER"), out var attach) && attach)
    Debugger.Launch();

await host.RunAsync();