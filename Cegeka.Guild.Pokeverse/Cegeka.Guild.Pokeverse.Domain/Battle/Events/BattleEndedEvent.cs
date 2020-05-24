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
            WinnerTrainerId = battle.Winner.TrainerId;
            WinnerPokemon = battle.Winner.Name;
            LoserTrainerId = battle.Loser.TrainerId;
            LoserPokemon = battle.Loser.Name;
        }

        public Guid BattleId { get; private set; }

        public Guid WinnerTrainerId { get; private set; }

        public string WinnerPokemon { get; private set; }

        public Guid LoserTrainerId { get; private set; }

        public string LoserPokemon { get; private set; }
    }
}