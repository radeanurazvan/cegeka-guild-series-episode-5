using Cegeka.Guild.Pokeverse.Domain;
using Cegeka.Guild.Pokeverse.Persistence.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            return services
                .AddScoped<IRepositoryMediator, RepositoryMediator>()
                .AddEntityFrameworkPersistence(configuration);
        }
    }
}
