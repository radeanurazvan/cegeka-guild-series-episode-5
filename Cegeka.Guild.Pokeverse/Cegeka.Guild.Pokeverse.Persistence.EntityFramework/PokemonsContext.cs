using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class PokemonsContext : DbContext
    {
        public PokemonsContext()
            : base(GetOptions())
        {
        }

        public PokemonsContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<PokemonDefinition> PokemonDefinitions { get; private set; }

        public DbSet<Trainer> Trainers { get; private set; }

        public DbSet<Battle> Battles { get; private set; }

        public static DbContextOptions GetOptions() => new DbContextOptionsBuilder()
            .UseSqlServer("Data Source=.; Initial Catalog=Pokeverse;User=sa;Password=Pass4Dev1!;")
            .Options;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokemonsContext).Assembly);
        }
    }
}