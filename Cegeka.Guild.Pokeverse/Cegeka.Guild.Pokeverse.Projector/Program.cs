using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Persistence.Mongo;
using Cegeka.Guild.Pokeverse.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cegeka.Guild.Pokeverse.Projector
{
    internal static class Program
    {
        public static Task Main(string[] args)
        {
            return new HostBuilder()
                .ConfigureLogging(b => b.AddConsole().AddDebug())
                .ConfigureServices(services => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build()
                .UseWithRabbitMqEventsLogging()
                .UseSubscriptions()
                .RunAsync();
        }

        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return services.AddSingleton<IConfiguration>(configuration)
                .AddMongoStorage()
                .AddRabbitMqBus()
                .AddProjectorSubscriptions()
                .AddProjectors();
        }
    }
}
