using System;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace Cegeka.Guild.Pokeverse.Persistence.Mongo
{
    internal sealed class MongoStorage : IMongoStorage
    {
        private readonly MongoSettings settings;
        private readonly MongoClient client;

        public MongoStorage(MongoSettings settings, MongoClient client)
        {
            this.settings = settings;
            this.client = client;
        }

        public Task Create<T>(T entity) where T : DocumentModel, new() => GetCollection<T>().InsertOneAsync(entity);

        public async Task Update<T>(string id, Action<T> update) where T : DocumentModel, new()
        {
            var collectionQuery = GetCollection<T>().AsQueryable();
            var replacement = await collectionQuery.Where(x => x.Id == id).FirstOrDefaultAsync();

            if (replacement == null)
            {
                return;
            }

            update(replacement);
            await GetCollection<T>().ReplaceOneAsync(x => x.Id == id, replacement);
        }

        public Task Update<T>(T instance) where T : DocumentModel, new() => GetCollection<T>().ReplaceOneAsync(x => x.Id == instance.Id, instance);

        public Task Delete<T>(string id) where T : DocumentModel, new() => GetCollection<T>().FindOneAndDeleteAsync(x => x.Id == id);

        private IMongoCollection<T> GetCollection<T>() where T : DocumentModel, new() => this.client.GetDatabase(this.settings.Database).GetCollection<T>(new T().GetCollectionName());
    }
}