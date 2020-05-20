using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public class Ability : Entity
    {
        public string Name { get; set; }

        public int Damage { get; set; }

        public int RequiredLevel { get; set; }
    }
}