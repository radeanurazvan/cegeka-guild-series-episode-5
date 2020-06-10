using System;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    internal sealed class EventMetadata
    {
        private EventMetadata()
        {
        }

        public EventMetadata(string aggregateId, Type eventType, Guid eventId)
        {
            AggregateId = aggregateId;
            ClrType = $"{eventType.FullName}, {eventType.Assembly.GetName().Name}";
            EventId = eventId;
        }

        public string AggregateId { get; private set; }

        public string ClrType { get; private set; }

        public Guid EventId { get; private set; }
    }
}