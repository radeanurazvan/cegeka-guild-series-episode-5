using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services
                .AddMediatR(BusinessAssembly.Value)
                .AddMessageHandler<BattleEndedEvent, BattleEndedEventHandler>()
                .AddMessageHandler<ExperienceGainedEvent, ExperienceGainedEventHandler>()
                .AddMessageHandler<TrainerRegisteredEvent, TrainerRegisteredEventHandler>();
        }
    }
}