using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Domain
{
    public interface IReadRepository<T>
        where T : AggregateRoot
    {
        Task<IEnumerable<T>> GetAll();

        Task<Maybe<T>> GetById(Guid id);
    }
}