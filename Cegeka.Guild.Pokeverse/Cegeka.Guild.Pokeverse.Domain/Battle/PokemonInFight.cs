using System;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class PokemonInFight
    {
        private PokemonInFight() {}

        public PokemonInFight(Pokemon pokemon)
            : this()
        {
            PokemonId = pokemon.Id;
            Pokemon = pokemon;
            Health = pokemon.Stats.HealthPoints * 15;
        }

        public Pokemon Pokemon { get; private set; }

        public Guid PokemonId { get; private set; }

        public int Health { get; private set; }

        internal void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}