using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseRabbitMqEvents(this IApplicationBuilder app, Action<RabbitMqEvents> act)
        {
            var events = app.ApplicationServices.GetService<RabbitMqEvents>();
            act(events);
            return app;
        }

        public static IApplicationBuilder UseRabbitMqEventsLogging(this IApplicationBuilder app)
        {
            var logger = app.ApplicationServices.GetService<ILogger<RabbitMqEvents>>();
            return app.UseRabbitMqEvents(events => events.OnConnectionFailure += (_, e) => logger.LogError($"Connecting to rabbit mq failed: {e.Message}"));
        }
    }
}