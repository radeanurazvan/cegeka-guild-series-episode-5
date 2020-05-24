using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using CSharpFunctionalExtensions;

namespace Cegeka.Guild.Pokeverse.Persistence.Mongo
{
    public interface IMongoStorage
    {
        Task<IEnumerable<T>> GetAll<T>()
            where T : DocumentModel, new();

        Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> predicate)
            where T : DocumentModel, new();

        Task<Maybe<T>> FindOne<T>(Expression<Func<T, bool>> predicate)
            where T : DocumentModel,  new();

        Task Create<T>(T entity) 
            where T : DocumentModel, new();
        
        Task Update<T>(string id, Action<T> update) 
            where T : DocumentModel, new();

        Task Update<T>(T instance) 
            where T : DocumentModel, new();
        
        Task Delete<T>(string id)
            where T : DocumentModel, new();
    }
}