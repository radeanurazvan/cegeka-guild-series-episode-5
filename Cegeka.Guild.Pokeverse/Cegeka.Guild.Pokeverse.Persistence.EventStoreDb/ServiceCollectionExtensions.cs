using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddEventStoreDb(this IServiceCollection services)
        {
            return services
                .AddScoped<AggregatesContext>()
                .AddSingleton(p => EventStoreConnectionFactory.Create())
                .AddSingleton(typeof(IStreamConfig<>), typeof(DefaultStreamConfig<>));
        }

        public static IServiceCollection AddEventSourcedRepository<T>(this IServiceCollection services)
            where T : AggregateRoot
        {
            return services.AddScoped<IReadRepository<T>, EventStoreReadRepository<T>>()
                .AddScoped<IWriteRepository<T>, EventStoreWriteRepository<T>>();
        }
    }
}