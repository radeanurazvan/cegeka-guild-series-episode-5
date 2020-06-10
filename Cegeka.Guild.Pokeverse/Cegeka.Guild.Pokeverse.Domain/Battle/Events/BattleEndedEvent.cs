using System;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class BattleEndedEvent : IDomainEvent
    {
        private BattleEndedEvent()
        {
        }

        internal BattleEndedEvent(Battle battle)
            : this()
        {
            BattleId = battle.Id;
            WinnerTrainerId = battle.Winner.Value.TrainerId;
            WinnerPokemon = battle.Winner.Value.Name;
            LoserTrainerId = battle.Loser.Value.TrainerId;
            LoserPokemon = battle.Loser.Value.Name;
            EndedAt = DateTime.UtcNow;
        }

        public Guid BattleId { get; private set; }

        public Guid WinnerTrainerId { get; private set; }

        public string WinnerPokemon { get; private set; }

        public Guid LoserTrainerId { get; private set; }

        public string LoserPokemon { get; private set; }

        public DateTime EndedAt { get; private set; }
    }
}