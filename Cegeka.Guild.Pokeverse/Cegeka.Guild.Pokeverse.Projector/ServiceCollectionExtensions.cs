using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Projector
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectorSubscriptions(this IServiceCollection services)
        {
            return services.AddSingleton<ProjectorSubscriptions>();
        }

        public static IServiceCollection AddProjectors(this IServiceCollection services)
        {
            return services
                .AddMessageHandler<TrainerRegisteredEvent, TrainerProjector>();
        }
    }
}