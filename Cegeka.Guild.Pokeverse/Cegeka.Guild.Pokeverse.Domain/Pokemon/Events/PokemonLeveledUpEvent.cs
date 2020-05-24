using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class PokemonLeveledUpEvent : IDomainEvent
    {
        private PokemonLeveledUpEvent() {}

        internal PokemonLeveledUpEvent(Pokemon pokemon)
        {
            PokemonId = pokemon.Id;
            TrainerId = pokemon.TrainerId;
            Level = pokemon.Level;
        }

        public Guid PokemonId { get; set; }

        public Guid TrainerId { get; private set; }

        public int Level { get; private set; }
    }
}