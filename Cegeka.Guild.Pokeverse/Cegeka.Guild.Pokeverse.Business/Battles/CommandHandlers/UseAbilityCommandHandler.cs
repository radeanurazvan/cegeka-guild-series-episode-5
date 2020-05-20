using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common.Resources;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class UseAbilityCommandHandler : IRequestHandler<UseAbilityCommand, Result>
    {
        private readonly IRepositoryMediator mediator;

        public UseAbilityCommandHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task<Result> Handle(UseAbilityCommand request, CancellationToken cancellationToken)
        {
            var player = this.mediator.Read<Pokemon>().GetById(request.ParticipantId).ToResult(Messages.PokemonDoesNotExist);
            return player.Bind(p => p.UseAbility(request.BattleId, request.AbilityId))
                .Tap(() => this.mediator.Write<Battle>().Save());
        }
    }
}