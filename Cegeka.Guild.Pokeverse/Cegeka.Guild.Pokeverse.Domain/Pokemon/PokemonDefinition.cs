using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class PokemonDefinition : AggregateRoot
    {
        public string Name { get; set; }

        public ICollection<Ability> Abilities { get; set; }
    }
}