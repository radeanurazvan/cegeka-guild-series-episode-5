using Microsoft.Extensions.DependencyInjection;

namespace Cegeka.Guild.Pokeverse.Common
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageHandler<TMessage, THandler>(this IServiceCollection services)
            where TMessage : IMessage
            where THandler : IMessageHandler<TMessage>
        {
            return services
                .AddScoped(typeof(THandler))
                .AddScoped(typeof(IMessageHandler<TMessage>), typeof(THandler));
        }

        public static IServiceCollection AddEventHandler<TMessage, THandler>(this IServiceCollection services)
            where TMessage : IDomainEvent
            where THandler : IEventHandler<TMessage>
        {
            return services
                .AddScoped(typeof(THandler))
                .AddScoped(typeof(IEventHandler<TMessage>), typeof(THandler));
        }
    }
}