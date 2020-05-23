using System.Threading.Tasks;

namespace Cegeka.Guild.Pokeverse.Common
{
    public interface IMessageHandler<in T>
        where T : IMessage
    {
        Task Handle(T @event);
    }
}