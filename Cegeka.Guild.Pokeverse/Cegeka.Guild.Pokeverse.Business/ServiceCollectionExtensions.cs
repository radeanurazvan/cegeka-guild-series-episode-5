using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Business
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            return services
                .AddMediatR(BusinessAssembly.Value);
        }
    }
}