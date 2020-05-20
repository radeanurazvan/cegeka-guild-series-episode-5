using System.Linq;
using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class BattleReadRepository : BaseEntityFrameworkGenericReadRepository<Battle>
    {
        public BattleReadRepository(PokemonsContext context) : base(context)
        {
        }

        protected override IQueryable<Battle> DecorateEntities(IQueryable<Battle> entities)
        {
            return entities
                .Include(x => x.Attacker)
                    .ThenInclude(x => x.Pokemon)
                    .ThenInclude(x => x.Definition)
                .Include(x => x.Defender)
                    .ThenInclude(x => x.Pokemon)
                    .ThenInclude(x => x.Definition)
                .Include(x => x.Winner)
                .Include(x => x.Loser);
        }
    }
}