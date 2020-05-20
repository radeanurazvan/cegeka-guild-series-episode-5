using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Persistence.InMemory
{
    internal class GenericReadRepository<T> : IReadRepository<T>, IWriteRepository<T>
        where T : AggregateRoot
    {
        private readonly ICollection<T> entities = new List<T>();

        public Task<IEnumerable<T>> GetAll() => Task.FromResult<IEnumerable<T>>(entities);

        public Task<Maybe<T>> GetById(Guid id) => Task.FromResult(entities.FirstOrNothing(e => e.Id == id));

        public Task Add(T entity)
        {
            entities.Add(entity);
            return Task.CompletedTask;
        }

        public Task Save() => Task.CompletedTask;
    }
}