using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    public interface IMessageHandler<in T>
        where T : IMessage
    {
        Task Handle(T message);
    }
}