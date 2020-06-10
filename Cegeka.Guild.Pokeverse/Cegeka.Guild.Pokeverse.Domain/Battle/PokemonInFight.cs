﻿using System;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class PokemonInFight
    {
        private PokemonInFight() {}

        internal PokemonInFight(Pokemon pokemon)
            : this()
        {
            Id = pokemon.Id;
            Name = pokemon.Name;
            TrainerId = pokemon.TrainerId;
            Health = pokemon.Stats.HealthPoints * 15;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public Guid TrainerId { get; private set; }

        public int Health { get; private set; }

        internal bool Fainted => Health <= 0;

        internal void TakeDamage(int damage)
        {
            Health -= damage;
        }
    }
}