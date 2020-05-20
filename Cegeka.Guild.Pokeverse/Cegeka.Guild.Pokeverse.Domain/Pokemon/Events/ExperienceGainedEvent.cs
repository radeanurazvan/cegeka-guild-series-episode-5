using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class ExperienceGainedEvent : IDomainEvent
    {
        private ExperienceGainedEvent()
        {
        }

        public ExperienceGainedEvent(Pokemon pokemon)
            : this()
        {
            PokemonId = pokemon.Id;
        }

        public Guid PokemonId { get; private set; }
    }
}