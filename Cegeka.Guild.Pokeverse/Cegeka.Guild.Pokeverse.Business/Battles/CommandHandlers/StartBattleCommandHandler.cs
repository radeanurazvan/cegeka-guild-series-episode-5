using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common.Resources;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class StartBattleCommandHandler : IRequestHandler<StartBattleCommand, Result>
    {
        private readonly IRepositoryMediator mediator;

        public StartBattleCommandHandler(IRepositoryMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<Result> Handle(StartBattleCommand request, CancellationToken cancellationToken)
        {
            var pokemons = this.mediator.Read<Pokemon>();
            var attackerResult = await pokemons.GetById(request.AttackerId).ToResult(Messages.PokemonDoesNotExist);
            var defenderResult = await pokemons.GetById(request.DefenderId).ToResult(Messages.PokemonDoesNotExist);

            var writeRepository = this.mediator.Write<Battle>();
            return await Result.FirstFailureOrSuccess(attackerResult, defenderResult)
                .Bind(() => attackerResult.Value.Attack(defenderResult.Value))
                .Tap(b => writeRepository.Add(b))
                .Tap(() => writeRepository.Save());
        }
    }
}