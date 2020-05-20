using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common.Resources;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class BattleEndedEventHandler : INotificationHandler<BattleEndedEvent>
    {
        private readonly IRepositoryMediator mediator;

        public BattleEndedEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(BattleEndedEvent notification, CancellationToken cancellationToken)
        {
            return this.mediator.Read<Battle>().GetById(notification.BattleId).ToResult(Messages.BattleDoesNotExist)
                .Bind(b => b.AwardParticipants())
                .Tap(() => this.mediator.Write<Battle>().Save());
        }
    }
}