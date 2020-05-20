using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseMigrations(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<PokemonsContext>();
                context.Database.Migrate();
            }
            return app;
        }
    }
}