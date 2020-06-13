using System;
using System.Threading;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Persistence.EventStoreDb;
using EventStore.ClientAPI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cegeka.Guild.Pokeverse.Projector.Subscriptions
{
    internal sealed class ProjectorHostedService : IHostedService
    {
        private const string SystemStreamPrefix = "$";

        private readonly IServiceProvider provider;
        private readonly IEventStoreConnection connection;

        private EventStoreCatchUpSubscription subscription;

        public ProjectorHostedService(IEventStoreConnection connection, IServiceProvider provider)
        {
            this.connection = connection;
            this.provider = provider;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            subscription = connection.SubscribeToStreamFrom("$ce-Battle", StreamCheckpoint.StreamStart, CatchUpSubscriptionSettings.Default, OnEventAppeared, null, OnSubscriptionDropped);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            subscription.Stop();
            return Task.CompletedTask;
        }

        private async Task OnEventAppeared(EventStoreCatchUpSubscription catchup, ResolvedEvent @event)
        {
            if (@event.Event.EventStreamId.StartsWith(SystemStreamPrefix))
            {
                return;
            }

            var metadata = JsonSerialization.Deserialize<EventMetadata>(@event.Event.Metadata);
            var eventType = Type.GetType(metadata.ClrType);

            var eventBody = JsonSerialization.Deserialize(@event.Event.Data, eventType);
            var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

            using (var scope = provider.CreateScope())
            {
                var registeredHandlers = scope.ServiceProvider.GetServices(handlerType);
                foreach (dynamic handler in registeredHandlers)
                {
                    try
                    {
                        await (Task)handler.Handle((dynamic)eventBody);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine($"Wooops, something went wrong, {e.Message} at {e.StackTrace}");
                    }
                }
            }
        }

        private void OnSubscriptionDropped(EventStoreCatchUpSubscription catchup, SubscriptionDropReason dropReason, Exception exception)
        {
        }
    }
}