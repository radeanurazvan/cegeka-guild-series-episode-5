using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Projector
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectorSubscriptions(this IServiceCollection services)
        {
            return services.AddSingleton<ProjectorSubscriptions>();
        }
    }
}