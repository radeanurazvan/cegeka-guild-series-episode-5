using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEntityFrameworkPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddDbContext<PokemonsContext>(options => options.UseSqlServer(configuration.GetConnectionString("PokeverseDatabase")))
                .AddScoped(typeof(IReadRepository<>), typeof(EntityFrameworkGenericReadRepository<>))
                .AddScoped<IReadRepository<Trainer>, TrainerReadRepository>()
                .AddScoped<IReadRepository<Battle>, BattleReadRepository>()
                .AddScoped<IReadRepository<Pokemon>, PokemonReadRepository>()
                .AddScoped(typeof(IWriteRepository<>), typeof(EntityFrameworkGenericWriteRepository<>))
                .AddScoped<ISeedService, SeedService>();
        }

    }
}