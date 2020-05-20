using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class GetFinishedBattlesQueryHandler : IRequestHandler<GetFinishedBattlesQuery, IEnumerable<FinishedBattleModel>>
    {
        private readonly IReadRepository<Battle> battleReadRepository;

        public GetFinishedBattlesQueryHandler(IReadRepository<Battle> battleReadRepository)
        {
            this.battleReadRepository = battleReadRepository;
        }

        public async Task<IEnumerable<FinishedBattleModel>> Handle(GetFinishedBattlesQuery request, CancellationToken cancellationToken)
        {
            return (await battleReadRepository.GetAll())
                .Where(b => b.Winner != null)
                .Select(b => new FinishedBattleModel
                {
                    Attacker = b.Attacker.Pokemon.Name,
                    Defender = b.Defender.Pokemon.Name,
                    StartedAt = b.StartedAt,
                    FinishedAt = b.FinishedAt,
                    Winner = b.Winner.Name
                });
        }
    }
}