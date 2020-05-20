using System.Linq;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using MediatR;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal sealed class EntityFrameworkGenericWriteRepository<T> : IWriteRepository<T>
        where T : AggregateRoot
    {
        private readonly PokemonsContext context;
        private readonly IMediator mediator;

        public EntityFrameworkGenericWriteRepository(PokemonsContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        public Task Add(T entity)
        {
            context.Set<T>().AddAsync(entity);
            return Task.CompletedTask;
        }

        public async Task Save()
        {
            await context.SaveChangesAsync();
            await ProcessEvents();
        }

        private async Task ProcessEvents()
        {
            var aggregatesWithEvents = this.context.ChangeTracker.Entries()
                .Where(e => e.Entity is AggregateRoot)
                .Select(e => e.Entity as AggregateRoot)
                .Where(a => a.Events.Any())
                .ToList();

            foreach (var aggregate in aggregatesWithEvents)
            {
                var events = aggregate.Events.ToList();
                aggregate.ClearEvents();

                var tasks = events.Select(e => this.mediator.Publish(e));
                await Task.WhenAll(tasks);
            }
        }
    }
}