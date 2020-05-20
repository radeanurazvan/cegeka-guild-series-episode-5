using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common.Resources;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class ExperienceGainedEventHandler : INotificationHandler<ExperienceGainedEvent>
    {
        private readonly IRepositoryMediator mediator;

        public ExperienceGainedEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(ExperienceGainedEvent notification, CancellationToken cancellationToken)
        {
            return this.mediator.Read<Pokemon>().GetById(notification.PokemonId).ToResult(Messages.PokemonDoesNotExist)
                .Bind(p => p.LevelUp())
                .Tap(() => this.mediator.Write<Pokemon>().Save());
        }
    }
}