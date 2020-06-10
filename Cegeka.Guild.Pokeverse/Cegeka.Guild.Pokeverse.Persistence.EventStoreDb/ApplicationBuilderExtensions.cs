using EventStore.ClientAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseEventStore(this IApplicationBuilder app)
        {
            var connection = app.ApplicationServices.GetRequiredService<IEventStoreConnection>();
            connection.ConnectAsync().GetAwaiter().GetResult();

            return app;
        }
    }
}