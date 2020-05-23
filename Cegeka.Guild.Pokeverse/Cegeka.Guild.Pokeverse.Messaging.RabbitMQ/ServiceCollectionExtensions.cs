using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;

namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddRabbitMqBus(this IServiceCollection services)
        {
            return services
                .AddSingleton<RabbitMqEvents>()
                .AddRabbitMqConnection()
                .AddRabbitMqSettings()
                .AddRabbitMqModel()
                .AddSingleton<IMessageBus, RabbitMqMessageBus>();
        }

        private static IServiceCollection AddRabbitMqConnection(this IServiceCollection services)
        {
            return services.AddSingleton<RabbitMqConnectionFactory>()
                .AddSingleton(p => p.GetService<RabbitMqConnectionFactory>().CreateConnection());
        }

        private static IServiceCollection AddRabbitMqModel(this IServiceCollection services)
        {
            return services.AddSingleton(p => p.GetService<IConnection>().CreateModel());
        }

        private static IServiceCollection AddRabbitMqSettings(this IServiceCollection services)
        {
            return services.AddSingleton(provider =>
                provider.GetService<IConfiguration>()
                    .GetSection(nameof(RabbitMqSettings))
                    .Get<RabbitMqSettings>(o => o.BindNonPublicProperties = true));
        }

    }
}