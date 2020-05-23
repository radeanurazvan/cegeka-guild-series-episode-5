using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    public static class HostExtensions
    {
        public static IHost UseWithRabbitMqEvents(this IHost host, Action<RabbitMqEvents> act)
        {
            var events = host.Services.GetService<RabbitMqEvents>();
            act(events);
            return host;
        }

        public static IHost UseWithRabbitMqEventsLogging(this IHost host)
        {
            var logger = host.Services.GetService<ILogger<RabbitMqEvents>>();
            return host.UseWithRabbitMqEvents(events => events.OnConnectionFailure += (_, e) => logger.LogError($"Connecting to rabbit mq failed: {e.Message}"));
        }
    }
}