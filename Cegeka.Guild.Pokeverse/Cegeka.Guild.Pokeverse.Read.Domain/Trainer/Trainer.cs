using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Read.Domain
{
    public class Trainer : DocumentModel
    {
        public Trainer()
        {
        }

        public override string GetCollectionName() => "trainers";

        public string Name { get; set; }

        public ICollection<Pokemon> Pokemons { get; set; }

        public ICollection<BattleHistory> BattleHistories { get; set; }
    }
}