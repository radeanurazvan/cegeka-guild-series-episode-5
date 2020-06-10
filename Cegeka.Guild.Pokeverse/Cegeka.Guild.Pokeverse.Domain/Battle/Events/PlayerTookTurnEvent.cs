using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    internal sealed class PlayerTookTurnEvent : IDomainEvent
    {
        private PlayerTookTurnEvent()
        {
        }

        public PlayerTookTurnEvent(Battle battle, Guid playerId, Ability ability)
            : this()
        {
            BattleId = battle.Id;
            PlayerId = playerId;
            Ability = new UsedAbility(ability);
        }

        public UsedAbility Ability { get; private set; }

        public Guid BattleId { get; private set; }

        public Guid PlayerId { get; private set; }

        internal sealed class UsedAbility
        {
            private UsedAbility()
            {
            }

            public UsedAbility(Ability ability)
                : this()
            {
                Id = ability.Id;
                Name = ability.Name;
                Damage = ability.Damage;
            }

            public Guid Id { get; private set; }

            public string Name { get; private set; }

            public int Damage { get; private set; }
        }
    }
}