using System;
using EventStore.ClientAPI;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    internal sealed class DecodedEvent
    {
        public DecodedEvent(RecordedEvent @event)
        {
            Metadata = JsonSerialization.Deserialize<EventMetadata>(@event.Metadata);
            Value = JsonSerialization.Deserialize(@event.Data, Type.GetType(Metadata.ClrType));
        }

        public EventMetadata Metadata { get; }

        public object Value { get; }
    }
}