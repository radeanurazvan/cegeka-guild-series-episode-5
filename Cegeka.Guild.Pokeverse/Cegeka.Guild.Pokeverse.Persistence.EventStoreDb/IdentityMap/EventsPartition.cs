using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    public class EventsPartition
    {
        public EventsPartition(IAggregateRoot aggregate)
        {
            Aggregate = aggregate;
            Events = aggregate.Events.ToList();
        }

        public IAggregateRoot Aggregate { get; private set; }

        public IEnumerable<IDomainEvent> Events { get; private set; }
    }
}