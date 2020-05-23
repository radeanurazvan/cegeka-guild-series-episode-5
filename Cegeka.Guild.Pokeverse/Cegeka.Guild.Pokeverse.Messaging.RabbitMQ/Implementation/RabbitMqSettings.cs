namespace Cegeka.Guild.Pokeverse.RabbitMQ
{
    internal sealed class RabbitMqSettings
    {
        public string Server { get; private set; }

        public int Port { get; private set; }

        public string Username { get; private set; }

        public string Password { get; private set; }
    }
}