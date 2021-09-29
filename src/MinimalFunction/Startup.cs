using System.Diagnostics;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

[assembly: FunctionsStartup(typeof(Startup))]

public class Startup : FunctionsStartup
{
    public override void ConfigureAppConfiguration(IFunctionsConfigurationBuilder config) 
    {
        if (Environment.GetEnvironmentVariable("ATTACH_DEBUGGER") is string debug &&
            bool.TryParse(debug, out var attach) && attach)
            Debugger.Launch();

        var context = config.GetContext();
        var env = context.EnvironmentName;
        var appPath = context.ApplicationRootPath;
        
        config.ConfigurationBuilder
                .AddJsonFile(Path.Combine(appPath, "appsettings.json"), optional: true, reloadOnChange: false)
                .AddDotNetConfig(appPath)
                .AddJsonFile(Path.Combine(appPath, $"appsettings.{env}.json"), optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine(appPath, "local.settings.json"), optional: true, reloadOnChange: true);
    }

    public override void Configure(IFunctionsHostBuilder builder)
    {
        // TODO: configure builder.Services
    }
}