using System;
using System.Collections.Generic;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Read.Domain.Battle
{
    public class Battle : DocumentModel
    {
        public PokemonInBattle Attacker { get; set; }

        public PokemonInBattle Defender { get; set; }

        public ICollection<string> Comments { get; set; } = new List<string>();

        public DateTime StartedAt { get; set; }

        public DateTime? EndedAt { get; set; }

        public override string GetCollectionName() => "Battles";
    }

    public sealed class PokemonInBattle
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public int Health { get; set; }
    }
}