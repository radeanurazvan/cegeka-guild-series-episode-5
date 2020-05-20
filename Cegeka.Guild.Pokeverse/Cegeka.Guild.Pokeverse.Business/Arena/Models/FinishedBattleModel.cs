using System;

namespace Cegeka.Guild.Pokeverse.Business
{
    public class FinishedBattleModel
    {
        public string Attacker { get; set; }

        public string Defender { get; set; }

        public string Winner { get; set; }

        public DateTime StartedAt { get; set; }

        public DateTime FinishedAt { get; set; }
    }
}