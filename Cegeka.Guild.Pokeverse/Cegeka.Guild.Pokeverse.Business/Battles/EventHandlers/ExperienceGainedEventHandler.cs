using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class ExperienceGainedEventHandler : IMessageHandler<ExperienceGainedEvent>
    {
        private readonly IRepositoryMediator mediator;

        public ExperienceGainedEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(ExperienceGainedEvent @event)
        {
            return this.mediator.Read<Pokemon>().GetById(@event.PokemonId).ToResult(Messages.PokemonDoesNotExist)
                .Bind(p => p.LevelUp())
                .Tap(() => this.mediator.Write<Pokemon>().Save());
        }
    }
}