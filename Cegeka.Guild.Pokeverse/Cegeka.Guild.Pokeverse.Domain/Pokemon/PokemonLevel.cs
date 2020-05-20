using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public sealed class PokemonLevel : ValueObject
    {
        private const int ExperienceThreshold = 100;

        private PokemonLevel()
        {
        }

        public static PokemonLevel Default() => new PokemonLevel {Current = 1, Experience = 0};

        public int Current { get; private set; }

        public int Experience { get; private set; }

        internal PokemonLevel WithMoreExperience(int experience)
        {
            return new PokemonLevel
            {
                Current = this.Current,
                Experience = this.Experience + experience
            };
        }

        internal Result<PokemonLevel> Next()
        {
            return Result.SuccessIf(Experience > Current * ExperienceThreshold, "Not enough experience")
                .Map(() => new PokemonLevel
                {
                    Current = this.Current + 1,
                    Experience = 0
                });
        }

        public static implicit operator int(PokemonLevel level) => level.Current;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Current;
            yield return Experience;
        }
    }
}