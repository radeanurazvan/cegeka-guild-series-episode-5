using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class BattleStartedEvent : IDomainEvent
    {
        private BattleStartedEvent()
        {
        }

        internal BattleStartedEvent(Battle battle, Pokemon attacker, Pokemon defender) 
            : this()
        {
            Id = battle.Id;
            Attacker = new BattlePokemon(new PokemonInFight(attacker));
            Defender = new BattlePokemon(new PokemonInFight(defender));
            StartedAt = DateTime.UtcNow;
        }

        public Guid Id { get; private set; }

        public BattlePokemon Attacker { get; private set; }

        public BattlePokemon Defender { get; private set; }

        public DateTime StartedAt { get; private set; }

        public sealed class BattlePokemon
        {
            private BattlePokemon()
            {
            }

            public BattlePokemon(PokemonInFight pokemon)
                : this()
            {
                Id = pokemon.Id;
                Name = pokemon.Name;
                TrainerId = pokemon.TrainerId;
                Health = pokemon.Health;
            }

            public Guid Id { get; private set; }

            public string Name { get; private set; }

            public Guid TrainerId { get; private set; }

            public int Health { get; private set; }
        }
    }
}