using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    public interface IStreamConfig<T>
        where T : IAggregateRoot
    {
        string GetStreamFor(string aggregateId);
    }
}