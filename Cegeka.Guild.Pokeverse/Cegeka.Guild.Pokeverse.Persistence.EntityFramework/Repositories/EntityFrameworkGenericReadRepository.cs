using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class EntityFrameworkGenericReadRepository<T> : BaseEntityFrameworkGenericReadRepository<T> where T : AggregateRoot
    {
        public EntityFrameworkGenericReadRepository(PokemonsContext context) : base(context)
        {
        }
    }
}