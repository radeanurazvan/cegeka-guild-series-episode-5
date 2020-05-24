namespace Cegeka.Guild.Pokeverse.Read.Domain
{
    public class Pokemon
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public PokemonStats Stats { get; set; }

        public int Level { get; set; }
    }
}