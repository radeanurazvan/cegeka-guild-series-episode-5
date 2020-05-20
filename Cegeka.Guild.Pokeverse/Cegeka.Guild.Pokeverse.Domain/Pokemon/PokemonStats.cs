using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class PokemonStats : ValueObject
    {
        private PokemonStats()
        {
        }

        public static PokemonStats Default => new PokemonStats
        {
            HealthPoints = 1,
            CriticalStrikeChancePoints = 0,
            DamagePoints = 2
        };

        public int HealthPoints { get; private set; }

        public int CriticalStrikeChancePoints { get; private set; }

        public int DamagePoints { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return HealthPoints;
            yield return CriticalStrikeChancePoints;
            yield return DamagePoints;
        }
    }
}