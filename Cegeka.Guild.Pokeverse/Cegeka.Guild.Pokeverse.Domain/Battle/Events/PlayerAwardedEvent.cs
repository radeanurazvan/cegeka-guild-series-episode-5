using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class PlayerAwardedEvent : IDomainEvent
    {
        private PlayerAwardedEvent()
        {
        }

        internal PlayerAwardedEvent(Battle battle, Guid playerId, int experiencePoints)
            : this()
        {
            BattleId = battle.Id;
            PlayerId = playerId;
            ExperiencePoints = experiencePoints;
        }

        public Guid BattleId { get; private set; }

        public Guid PlayerId { get; private set; }

        public int ExperiencePoints { get; private set; }
    }
}