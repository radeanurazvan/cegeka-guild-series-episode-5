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

        public async Task<Result> Handle(UseAbilityCommand request, CancellationToken cancellationToken)
        {
            var battleResult = await this.mediator.Read<Battle>().GetById(request.BattleId).ToResult(Messages.BattleDoesNotExist);
            var abilityResult = await this.mediator.Read<Pokemon>().GetById(request.ParticipantId).ToResult(Messages.PokemonDoesNotExist)
                .Map(p => p.Abilities.TryFirst(a => a.Id == request.AbilityId))
                .Bind(a => a.ToResult(Messages.UnknownAbility));

            return await Result.FirstFailureOrSuccess(battleResult, abilityResult)
                .Bind(() => battleResult.Value.TakeTurn(request.ParticipantId, abilityResult.Value))
                .Tap(() => mediator.Write<Battle>().Save());
        }
    }
}