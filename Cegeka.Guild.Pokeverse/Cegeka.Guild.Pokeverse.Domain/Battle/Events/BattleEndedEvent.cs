using System;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;

namespace Cegeka.Guild.Pokeverse.Business
{
    public sealed class BattleEndedEvent : IDomainEvent
    {
        private BattleEndedEvent()
        {
        }

        internal BattleEndedEvent(Battle battle)
        {
            BattleId = battle.Id;
        }

        public Guid BattleId { get; }
    }
}