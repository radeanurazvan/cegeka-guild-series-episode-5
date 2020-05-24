using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Cegeka.Guild.Pokeverse.Common;
using CSharpFunctionalExtensions;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using Polly;

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

        public async Task<IEnumerable<T>> GetAll<T>()
            where T : DocumentModel, new()
        {
            return await GetCollection<T>().AsQueryable().ToListAsync();
        }

        public Task<IEnumerable<T>> Find<T>(Expression<Func<T, bool>> predicate)
            where T : DocumentModel, new()
        {
            var collectionQuery = GetCollection<T>().AsQueryable();
            return Policy<IEnumerable<T>>.Handle<Exception>()
                .FallbackAsync(collectionQuery.Where(predicate.Compile()))
                .ExecuteAsync(async () => await collectionQuery.Where(predicate).ToListAsync());
        }

        public async Task<Maybe<T>> FindOne<T>(Expression<Func<T, bool>> predicate) 
            where T : DocumentModel, new()
        {
            var aggregateOrNothing = Maybe<T>.None;
            var collectionQuery = GetCollection<T>().AsQueryable();
            try
            {
                aggregateOrNothing = await collectionQuery.FirstOrDefaultAsync(predicate);
            }
            catch
            {
                aggregateOrNothing = collectionQuery.Where(predicate.Compile()).FirstOrDefault();
            }

            return aggregateOrNothing;
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