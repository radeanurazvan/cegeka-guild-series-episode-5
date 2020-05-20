using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public class BattleConfiguration : IEntityTypeConfiguration<Battle>
    {
        public void Configure(EntityTypeBuilder<Battle> builder)
        {
            builder.HasOne(battle => battle.Attacker)
                .WithOne()
                .HasPrincipalKey<PokemonInFight>(p => p.PokemonId)
                .HasForeignKey<Battle>(b => b.AttackerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(battle => battle.Defender)
                .WithOne()
                .HasPrincipalKey<PokemonInFight>(p => p.PokemonId)
                .HasForeignKey<Battle>(b => b.DefenderId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(x => x.Winner)
                .WithOne()
                .HasForeignKey<Battle>(x => x.WinnerId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Ignore(b => b.IsOnGoing);
            builder.HasOne(x => x.Loser)
                .WithOne()
                .HasForeignKey<Battle>(x => x.LoserId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}