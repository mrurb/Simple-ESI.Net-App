using ESI.NET;

using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static void Main(string[] args)
    {
        IHost host = new HostBuilder()
        .ConfigureFunctionsWebApplication()
        .ConfigureServices(services =>
        {
            IServiceCollection unused2 = services.AddApplicationInsightsTelemetryWorkerService();
            IServiceCollection unused1 = services.ConfigureFunctionsApplicationInsights();

            IConfigurationRoot config = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("local.settings.json", optional: true, reloadOnChange: true)
                   .AddEnvironmentVariables()
                   .Build();

            IConfigurationSection section = config.GetSection("ESIConfig");
            IServiceCollection unused = services.AddEsi(section);
        })
        .Build();

        host.Run();
    }
}