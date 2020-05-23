using System;

namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    public sealed class RabbitMqEvents
    {
        public event EventHandler<Exception> OnConnectionFailure;

        internal void RaiseConnectionFailure(Exception e)
        {
            OnConnectionFailure?.Invoke(this, e);
        }
    }
}