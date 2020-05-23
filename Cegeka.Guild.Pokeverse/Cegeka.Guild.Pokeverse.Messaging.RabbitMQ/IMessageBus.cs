using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    public interface IMessageBus
    {
        Task Publish<T>(T message) where T : IMessage;

        Task Subscribe<T>() where T : IMessage;
    }
}