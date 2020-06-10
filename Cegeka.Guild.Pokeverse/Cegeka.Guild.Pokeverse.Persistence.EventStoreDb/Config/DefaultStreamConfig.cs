using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    internal sealed class DefaultStreamConfig<T> : IStreamConfig<T>
        where T : IAggregateRoot
    {
        public string GetStreamFor(string aggregateId)
        {
            return $"{typeof(T).Name}-{aggregateId}";
        }
    }
}