using System;

namespace Cegeka.Guild.Pokeverse.Domain
{
    internal static class BattleExperience
    {
        private const int MinExperienceGainedValue = 40;
        private const int MaxExperienceGainedValue = 50;
        private const double WinnerBonusFactor = 0.5;

        public static int ForWinner()
        {
            var baseExperience = ForLoser();
            return (int) Math.Round(baseExperience * WinnerBonusFactor) + baseExperience;
        }

        public static int ForLoser()
        {
            return new Random(DateTime.Now.Millisecond).Next(MinExperienceGainedValue, MaxExperienceGainedValue);
        }
    }
}