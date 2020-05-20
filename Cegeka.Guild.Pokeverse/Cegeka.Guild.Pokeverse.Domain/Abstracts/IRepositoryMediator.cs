using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public interface IRepositoryMediator
    {
        IReadRepository<T> Read<T>() where T : AggregateRoot;

        IWriteRepository<T> Write<T>() where T: AggregateRoot;
    }
}