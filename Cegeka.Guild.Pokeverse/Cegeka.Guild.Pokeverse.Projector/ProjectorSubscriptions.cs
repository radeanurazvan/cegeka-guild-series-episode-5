using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using Cegeka.Guild.Pokeverse.RabbitMQ;

namespace Cegeka.Guild.Pokeverse.Projector
{
    internal sealed class ProjectorSubscriptions
    {
        private readonly IMessageBus messageBus;

        public ProjectorSubscriptions(IMessageBus messageBus)
        {
            this.messageBus = messageBus;
        }

        public async Task Subscribe()
        {
            await messageBus.Subscribe<TrainerRegisteredEvent>();
            await messageBus.Subscribe<ExperienceGainedEvent>();
            await messageBus.Subscribe<BattleEndedEvent>();
            await messageBus.Subscribe<PokemonCreatedEvent>();
            await messageBus.Subscribe<PokemonLeveledUpEvent>();
        }
    }
}