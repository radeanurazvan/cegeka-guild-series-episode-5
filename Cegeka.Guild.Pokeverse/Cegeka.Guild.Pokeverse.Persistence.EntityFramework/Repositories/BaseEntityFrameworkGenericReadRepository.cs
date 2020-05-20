using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using Cegeka.Guild.Pokeverse.Domain;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Guild.Pokeverse.Persistence.EntityFramework
{
    internal abstract class BaseEntityFrameworkGenericReadRepository<T> : IReadRepository<T>
        where T : AggregateRoot
    {
        private readonly IQueryable<T> entities;

        protected BaseEntityFrameworkGenericReadRepository(PokemonsContext context)
        {
            this.entities = this.DecorateEntities(context.Set<T>());
        }

        protected virtual IQueryable<T> DecorateEntities(IQueryable<T> entities) => entities;

        public async Task<IEnumerable<T>> GetAll()
        {
            var list = await entities.AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<Maybe<T>> GetById(Guid id)
        {
            return await entities.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}