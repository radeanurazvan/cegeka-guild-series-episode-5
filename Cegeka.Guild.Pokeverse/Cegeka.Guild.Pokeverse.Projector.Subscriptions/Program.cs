using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Persistence.EventStoreDb;
using Cegeka.Guild.Pokeverse.Persistence.Mongo;
using Cegeka.Guild.Pokeverse.RabbitMQ.ContractResolvers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Cegeka.Guild.Pokeverse.Projector.Subscriptions
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
                .UseEventStore()
                .RunAsync();
        }

        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var contractResolver = new PrivateCamelCasePropertyContractResolver();
            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
                ContractResolver = contractResolver
            };

            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return services
                .AddHostedService<ProjectorHostedService>()
                .AddSingleton<IConfiguration>(configuration)
                .AddMongoStorage()
                .AddEventStoreDb()
                .AddProjectors();
        }
    }
}
