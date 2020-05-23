using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cegeka.Guild.Pokeverse.Projector
{
    internal static class HostExtensions
    {
        public static IHost UseSubscriptions(this IHost host)
        {
            var subscriptions = host.Services.GetService<ProjectorSubscriptions>();
            subscriptions.Subscribe().GetAwaiter().GetResult();

            return host;
        }
    }
}