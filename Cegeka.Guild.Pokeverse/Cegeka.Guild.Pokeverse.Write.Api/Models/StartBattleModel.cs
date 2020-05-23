using System;

namespace Cegeka.Guild.Pokeverse.Api
{
    public class StartBattleModel
    {
        public Guid AttackerId { get; set; }

        public Guid DefenderId { get; set; }
    }
}