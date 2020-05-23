using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Common.Resources;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class BattleEndedEventHandler : IMessageHandler<BattleEndedEvent>
    {
        private readonly IRepositoryMediator mediator;

        public BattleEndedEventHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task Handle(BattleEndedEvent @event)
        {
            return this.mediator.Read<Battle>().GetById(@event.BattleId).ToResult(Messages.BattleDoesNotExist)
                .Bind(b => b.AwardParticipants())
                .Tap(() => this.mediator.Write<Battle>().Save());
        }
    }
}