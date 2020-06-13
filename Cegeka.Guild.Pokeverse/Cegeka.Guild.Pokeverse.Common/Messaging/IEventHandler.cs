using System.Threading.Tasks;

namespace Cegeka.Guild.Pokeverse.Common
{
    public interface IEventHandler<in T>
        where T : IDomainEvent
    {
        Task Handle(T @event);
    }
}