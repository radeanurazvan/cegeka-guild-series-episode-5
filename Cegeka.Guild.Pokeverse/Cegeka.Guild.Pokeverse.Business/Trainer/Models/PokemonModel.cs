using System;
using System.Collections.Generic;

namespace Cegeka.Guild.Pokeverse.Business
{
    public class PokemonModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public IEnumerable<AbilityModel> Abilities { get; set; }
    }
}