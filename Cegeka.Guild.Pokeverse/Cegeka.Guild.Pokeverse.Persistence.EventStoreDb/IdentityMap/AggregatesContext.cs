using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Common;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    public sealed class AggregatesContext
    {
        private readonly ICollection<IAggregateRoot> attachedAggregates = new List<IAggregateRoot>();
        public IEnumerable<IAggregateRoot> AttachedAggregates => attachedAggregates.Concat(attachedAggregates.SelectMany(a => a.GetNestedAggregates()));

        public static AggregatesContext WithAggregates(IEnumerable<IAggregateRoot> aggregates)
        {
            var context = new AggregatesContext();
            foreach (var aggregateRoot in aggregates)
            {
                context.Attach(aggregateRoot);
            }

            return context;
        }

        public void Attach(IAggregateRoot aggregate)
        {
            var duplicateAggregate = this.AttachedAggregates.TryFirst(a => a.GetId() == aggregate.GetId());
            Result.SuccessIf(duplicateAggregate.HasNoValue, "Aggregate already exists")
                .Ensure(() => aggregate != null, "Invalid aggregate")
                .Tap(() => attachedAggregates.Add(aggregate));
        }

        public Result<T> GetById<T>(string id)
            where T : class, IAggregateRoot
        {
            return this.AttachedAggregates.TryFirst(a => a.GetId() == id)
                .ToResult("No aggregate attached")
                .Map(a => a as T);
        }

        public void Clear()
        {
            this.attachedAggregates.Clear();
        }

        public IReadOnlyCollection<EventsPartition> SplitIntoPartitions()
        {
            var partitions = AttachedAggregates.Select(a => new EventsPartition(a)).ToList();
            foreach (var attachedAggregate in AttachedAggregates)
            {
                attachedAggregate.ClearEvents();
            }

            return partitions;
        }
    }
}