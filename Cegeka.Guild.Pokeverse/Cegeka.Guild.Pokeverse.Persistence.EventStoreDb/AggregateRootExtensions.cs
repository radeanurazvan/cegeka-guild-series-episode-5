using System.Collections.Generic;
using System.Linq;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    public static class AggregateRootExtensions
    {
        public static IEnumerable<IAggregateRoot> GetNestedAggregates<T>(this T aggregate)
            where T : class, IAggregateRoot
        {
            var currentAggregateType = aggregate.GetType();

            var nestedAggregatesProperties = currentAggregateType
                .GetProperties()
                .Where(p => typeof(IAggregateRoot).IsAssignableFrom(p.PropertyType));

            var nestedAggregates = nestedAggregatesProperties
                .Select(p => p.GetValue(aggregate) as IAggregateRoot)
                .Where(a => a != null);

            var result = new List<IAggregateRoot>();
            foreach (var nestedAggregate in nestedAggregates)
            {
                result.Add(nestedAggregate);
                result.AddRange(GetNestedAggregates(nestedAggregate));
            }

            return result;
        }
    }
}