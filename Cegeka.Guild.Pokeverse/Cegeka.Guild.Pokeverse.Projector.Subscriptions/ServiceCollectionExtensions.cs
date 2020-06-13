using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using Cegeka.Guild.Pokeverse.Projector.Subscriptions.Projectors;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Projector
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddProjectors(this IServiceCollection services)
        {
            return services
                .AddEventHandler<BattleStartedEvent, BattleProjector>()
                .AddEventHandler<PlayerTookTurnEvent, BattleProjector>()
                .AddEventHandler<BattleEndedEvent, BattleProjector>()
                .AddEventHandler<PlayerAwardedEvent, BattleProjector>();
        }
    }
}