using System;

namespace Cegeka.Guild.Pokeverse.Business
{
    public class OngoingBattleModel
    {
        public Guid Id { get; set; }

        public string Attacker { get; set; }

        public int AttackerHealth { get; set; }

        public string Defender { get; set; }

        public int DefenderHealth { get; set; }

        public DateTime StartedAt { get; set; }
    }
}