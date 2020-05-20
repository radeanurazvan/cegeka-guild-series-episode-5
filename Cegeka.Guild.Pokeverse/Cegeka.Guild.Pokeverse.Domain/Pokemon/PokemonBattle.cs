using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class PokemonBattle : Entity
    {
        private PokemonBattle()
        {
        }

        internal PokemonBattle(Pokemon pokemon, Battle battle)
            : this()
        {
            BattleId = battle.Id;
            Battle = battle;
            PokemonId = pokemon.Id;
        }

        public Guid BattleId { get; private set; }

        public Battle Battle { get; private set; }

        public Guid PokemonId { get; private set; }
    }
}