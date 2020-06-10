using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace Cegeka.Guild.Pokeverse.Persistence.EventStoreDb
{
    internal class EventStoreReadRepository<T> : IReadRepository<T>
        where T : AggregateRoot
    {
        private const int sliceReadSize = 1024;

        private readonly AggregatesContext context;
        private readonly IStreamConfig<T> streamConfig;
        private readonly IEventStoreConnection connection;

        public EventStoreReadRepository(IEventStoreConnection connection, IStreamConfig<T> streamConfig, AggregatesContext context)
        {
            this.context = context;
            this.connection = connection;
            this.streamConfig = streamConfig;
        }


        public Task<IEnumerable<T>> GetAll()
        {
            throw new NotImplementedException("Event store does not support wide queries");
        }

        public Task<Maybe<T>> GetById(Guid id)
        {
            return context.GetById<T>(id.ToString())
                .OnFailureCompensate(() => GetFromStore(id))
                .Ensure(a => !a.IsDeleted, "Entity is deleted")
                .ToMaybe();
        }

        private async Task<Result<T>> GetFromStore(Guid id)
        {
            var aggregate = (T)Activator.CreateInstance(typeof(T), true);
            var stream = this.streamConfig.GetStreamFor(id.ToString());

            var sliceStart = 0L;
            var canContinue = true;

            while (canContinue)
            {
                await Result.Ok()
                    .Map(() => connection.ReadStreamEventsForwardAsync(stream, sliceStart, sliceReadSize, false))
                    .Tap(sr => canContinue = sr.Status != SliceReadStatus.StreamNotFound && sr.Status != SliceReadStatus.StreamDeleted)
                    .Ensure(_ => canContinue, "Stream not found")
                    .Tap(sr =>
                    {
                        foreach (var resolvedEvent in sr.Events)
                        {
                            var decodedEvent = new DecodedEvent(resolvedEvent.OriginalEvent);
                            aggregate.Mutate(decodedEvent.Value as IDomainEvent);
                        }
                    })
                    .Tap(s => sliceStart += s.Events.Length)
                    .Tap(sr => canContinue = !sr.IsEndOfStream)
                    .Ensure(_ => canContinue, "Reached end of stream");
            }

            return Result.FailureIf(sliceStart == 0, aggregate, "No events found for given aggregate")
                .Tap(a => a.ClearEvents())
                .Tap(a => context.Attach(a));
        }
    }
}