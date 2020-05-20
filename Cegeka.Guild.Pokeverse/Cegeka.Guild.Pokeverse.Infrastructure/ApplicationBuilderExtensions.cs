using Cegeka.Guild.Pokeverse.Persistence.EntityFramework;
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
    }
}