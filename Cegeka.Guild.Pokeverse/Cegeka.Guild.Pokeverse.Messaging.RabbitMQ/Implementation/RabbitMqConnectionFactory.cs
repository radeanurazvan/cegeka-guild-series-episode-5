using System;
using Polly;
using RabbitMQ.Client;

namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    internal sealed class RabbitMqConnectionFactory
    {
        private readonly RabbitMqSettings settings;
        private readonly RabbitMqEvents events;

        public RabbitMqConnectionFactory(RabbitMqSettings settings, RabbitMqEvents events)
        {
            this.settings = settings;
            this.events = events;
        }

        public IConnection CreateConnection()
        {
            return Policy.Handle<Exception>()
                .WaitAndRetry(50, attempt => TimeSpan.FromSeconds(1), (e, _) => events.RaiseConnectionFailure(e))
                .ExecuteAndCapture(() => new ConnectionFactory
                {
                    HostName = settings.Server,
                    Port = settings.Port,
                    UserName = settings.Username,
                    Password = settings.Password
                }.CreateConnection())
                .Result;
        }
    }
}