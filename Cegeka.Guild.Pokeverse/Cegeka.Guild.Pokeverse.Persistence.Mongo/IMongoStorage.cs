using System;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;

namespace Cegeka.Guild.Pokeverse.Persistence.Mongo
{
    public interface IMongoStorage
    {
        Task Create<T>(T entity) where T : DocumentModel, new();
        
        Task Update<T>(string id, Action<T> update) where T : DocumentModel, new();

        Task Update<T>(T instance) where T : DocumentModel, new();
        
        Task Delete<T>(string id) where T : DocumentModel, new();
    }
}