using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class TrainerConfiguration : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            builder.HasKey(t => t.Id);

            builder.Ignore(t => t.Pokemons);
            builder.HasMany(Trainer.Expressions.Pokemons)
                .WithOne()
                .HasPrincipalKey(nameof(Trainer.Id))
                .HasForeignKey(nameof(Pokemon.TrainerId));
        }
    }
}