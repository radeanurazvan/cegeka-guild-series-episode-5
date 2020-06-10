using System;
using EventStore.ClientAPI;
using EventStore.ClientAPI.SystemData;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    public class EventStoreConnectionFactory
    {
        private const string ConnectionString = "ConnectTo=tcp://admin:changeit@localhost:1113";

        public static IEventStoreConnection Create()
        {
            var settingsBuilder = ConnectionSettings.Create()
                //.KeepReconnecting()
                .LimitRetriesForOperationTo(50)
                .SetReconnectionDelayTo(TimeSpan.FromSeconds(1))
                .SetDefaultUserCredentials(new UserCredentials("admin", "changeit"));

            return EventStoreConnection.Create(ConnectionString, settingsBuilder, "Cegeka Guild series");
        }
    }
}