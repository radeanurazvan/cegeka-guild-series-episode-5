using System.Linq;
using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class PokemonReadRepository : BaseEntityFrameworkGenericReadRepository<Pokemon>
    {
        public PokemonReadRepository(PokemonsContext context) : base(context)
        {
        }

        protected override IQueryable<Pokemon> DecorateEntities(IQueryable<Pokemon> entities)
        {
            return entities
                .Include(pokemon => pokemon.Definition)
                    .ThenInclude(definition => definition.Abilities);
        }
    }
}