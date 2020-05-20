using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public class PokemonConfiguration : IEntityTypeConfiguration<Pokemon>
    {
        public void Configure(EntityTypeBuilder<Pokemon> builder)
        {
            builder.HasOne<Trainer>()
                .WithMany(x => x.Pokemons)
                .HasForeignKey(x => x.TrainerId);

            builder.Ignore(x => x.Abilities);
            builder.HasOne(x => x.Definition)
                .WithMany()
                .HasForeignKey(x => x.DefinitionId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(p => p.Battles);
            builder.HasMany(Pokemon.Expressions.Battles)
                .WithOne()
                .HasPrincipalKey(nameof(Pokemon.Id))
                .HasForeignKey(nameof(PokemonBattle.PokemonId));

            builder.OwnsOne(p => p.Stats, s =>
            {
                s.Property(ps => ps.DamagePoints);
                s.Property(ps => ps.CriticalStrikeChancePoints);
                s.Property(ps => ps.HealthPoints);
            });

            builder.OwnsOne(p => p.Level, l =>
            {
                l.Property(x => x.Current);
                l.Property(x => x.Experience);
            });
        }
    }
}