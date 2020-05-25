using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class PokemonDefinitionConfiguration : IEntityTypeConfiguration<PokemonDefinition>
    {
        public void Configure(EntityTypeBuilder<PokemonDefinition> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.Id).ValueGeneratedNever();

            builder.HasMany(typeof(Pokemon))
                .WithOne(nameof(Pokemon.Definition))
                .HasForeignKey(nameof(Pokemon.DefinitionId));
        }
    }
}