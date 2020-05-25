using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class PokemonBattleConfiguration : IEntityTypeConfiguration<PokemonBattle>
    {
        public void Configure(EntityTypeBuilder<PokemonBattle> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id).ValueGeneratedNever();

            builder.HasOne(typeof(Pokemon))
                .WithMany(Pokemon.Expressions.Battles)
                .HasPrincipalKey(nameof(Pokemon.Id))
                .HasForeignKey(nameof(PokemonBattle.PokemonId));

            builder.HasOne(b => b.Battle)
                .WithMany()
                .HasPrincipalKey(nameof(Battle.Id))
                .HasForeignKey(nameof(PokemonBattle.BattleId));
        }
    }
}