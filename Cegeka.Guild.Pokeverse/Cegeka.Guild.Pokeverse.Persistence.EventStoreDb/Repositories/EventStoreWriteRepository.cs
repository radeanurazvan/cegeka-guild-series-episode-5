using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using Cegeka.Guild.Pokeverse.RabbitMQ;
using EventStore.ClientAPI;
using Newtonsoft.Json;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    internal sealed class EventStoreWriteRepository<T> : IWriteRepository<T>
        where T : AggregateRoot
    {
        private readonly AggregatesContext context;
        private readonly IStreamConfig<T> streamConfig;
        private readonly IEventStoreConnection connection;
        private readonly IMessageBus messageBus;

        public EventStoreWriteRepository(AggregatesContext context, IStreamConfig<T> streamConfig, IEventStoreConnection connection, IMessageBus messageBus)
        {
            this.context = context;
            this.connection = connection;
            this.messageBus = messageBus;
            this.streamConfig = streamConfig;
        }

        public Task Add(T aggregate)
        {
            context.Attach(aggregate);
            return Task.CompletedTask;
        }

        public async Task Save()
        {
            var tasks = this.context.SplitIntoPartitions().Select(async p =>
            {
                await StoreEvents(p);
                await PublishEvents(p);
            });

            await Task.WhenAll(tasks);
            this.context.Clear();
        }

        private Task StoreEvents(EventsPartition partition)
        {
            var partitionAggregate = partition.Aggregate;
            var stream = streamConfig.GetStreamFor(partitionAggregate.GetId());
            var events = partition.Events.Select(e => CreateEventData(e, partitionAggregate.GetId()));

            return connection.AppendToStreamAsync(stream, ExpectedVersion.Any, events);
        }

        private Task PublishEvents(EventsPartition partition)
        {
            var tasks = partition.Events.Select(messageBus.Publish);
            return Task.WhenAll(tasks);
        }

        private EventData CreateEventData(IDomainEvent @event, string aggregateId)
        {
            var eventId = Guid.NewGuid();
            var metadata = new EventMetadata(aggregateId, @event.GetType(), eventId);
            var metadataBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(metadata));
            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

            return new EventData(eventId, @event.GetType().GetFriendlyName(), true, data, metadataBytes);
        }
    }
}