using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cegeka.Guild.Pokeverse.Projector
{
    internal static class Program
    {
        public static Task Main(string[] args)
        {
            return new HostBuilder()
                .ConfigureServices(services => ConfigureServices(services))
                .UseConsoleLifetime()
                .Build()
                .RunAsync();
        }

        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
            return services.AddSingleton<IConfiguration>(configuration);
        }
    }
}
