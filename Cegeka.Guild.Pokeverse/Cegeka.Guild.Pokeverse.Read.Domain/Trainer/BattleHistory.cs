namespace Cegeka.Guild.Pokeverse.Read.Domain
{
    public class BattleHistory
    {
        public string BattleId { get; set; }

        public BattleStatus Status { get; set; }
        
        public int ExperienceGained { get; set; }

        public string Pokemon { get; set; }

        public string PokemonId { get; set; }
    }

    public enum BattleStatus
    {
        Won,
        Lost
    }
}