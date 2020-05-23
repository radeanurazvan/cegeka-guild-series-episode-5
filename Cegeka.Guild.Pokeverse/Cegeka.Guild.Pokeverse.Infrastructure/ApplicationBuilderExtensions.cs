using Cegeka.Guild.Pokeverse.Business;
using Cegeka.Guild.Pokeverse.Domain;
using Cegeka.Guild.Pokeverse.Persistence.EntityFramework;
using Cegeka.Guild.Pokeverse.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var seedService = scope.ServiceProvider.GetService<ISeedService>();
                seedService.Seed().GetAwaiter().GetResult();
            }
            
            return app;
        }

        public static IApplicationBuilder UseSubscriptions(this IApplicationBuilder app)
        {
            var messageBus = app.ApplicationServices.GetService<IMessageBus>();
            messageBus.Subscribe<BattleEndedEvent>().GetAwaiter().GetResult();
            messageBus.Subscribe<TrainerRegisteredEvent>().GetAwaiter().GetResult();
            messageBus.Subscribe<ExperienceGainedEvent>().GetAwaiter().GetResult();

            return app;
        }
    }
}