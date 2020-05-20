using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Business
{
    internal sealed class GetOngoingBattlesQueryHandler : IRequestHandler<GetOngoingBattlesQuery, IEnumerable<OngoingBattleModel>>
    {
        private readonly IReadRepository<Battle> battleReadRepository;

        public GetOngoingBattlesQueryHandler(IReadRepository<Battle> battleReadRepository)
        {
            this.battleReadRepository = battleReadRepository;
        }

        public async Task<IEnumerable<OngoingBattleModel>> Handle(GetOngoingBattlesQuery request, CancellationToken cancellationToken)
        {
            return (await battleReadRepository.GetAll())
                .Where(b => b.Winner == null)
                .Select(b => new OngoingBattleModel
                {
                    Id = b.Id,
                    Attacker = b.Attacker.Pokemon.Name,
                    AttackerHealth = b.Attacker.Health,
                    Defender = b.Defender.Pokemon.Name,
                    DefenderHealth = b.Defender.Health,
                    StartedAt = b.StartedAt
                });
        }
    }
}