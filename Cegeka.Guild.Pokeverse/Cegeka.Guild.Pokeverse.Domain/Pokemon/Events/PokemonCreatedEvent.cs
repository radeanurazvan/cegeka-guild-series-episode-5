using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class PokemonCreatedEvent : IDomainEvent
    {
        private PokemonCreatedEvent() {}

        internal PokemonCreatedEvent(Pokemon pokemon)
        {
            Id = pokemon.Id;
            Name = pokemon.Name;
            HealthPoints = pokemon.Stats.HealthPoints;
            CriticalStrikeChancePoints = pokemon.Stats.CriticalStrikeChancePoints;
            DamagePoints = pokemon.Stats.DamagePoints;
            TrainerId = pokemon.TrainerId;
        }
        
        public Guid Id { get; private set; }

        public Guid TrainerId { get; private set; }

        public string Name { get; private set; }

        public int HealthPoints { get; private set; }

        public int CriticalStrikeChancePoints { get; private set; }

        public int DamagePoints { get; private set; }
    }
}