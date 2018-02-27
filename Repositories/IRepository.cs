using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HtHInAction.Models;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace HtHInAction.Repositories
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> Get(string id);

        Task Add(T item);

        Task<bool> Update(string id, T item);

        Task<bool> Remove(string id);

        Task<IEnumerable<T>> Find(Expression<Func<T,bool>> filter);
    }

    public class Repository<T> : IRepository<T> where T : BaseItem
    {
        private readonly IMongoCollection<T> _collection;

        public Repository(IOptions<Settings> settings)
        {
            var client =  new MongoClient(settings.Value.ConnectionString);
           var database = client.GetDatabase(settings.Value.Database);
           _collection = database.GetCollection<T>(typeof(T).Name); 
        }

        public async Task Add(T item)
        {
            await _collection.InsertOneAsync(item);
        }

        public async Task<T> Get(string id)
        {
            return await _collection.Find( x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _collection.Find(_=> true).ToListAsync();
        }

        public async Task<bool> Remove(string id)
        {
            DeleteResult actionResult = await _collection.DeleteOneAsync(x=> x.Id == id);
            return actionResult.IsAcknowledged && actionResult.DeletedCount > 0;
        }

        public async Task<bool> Update(string id, T item)
        {
            ReplaceOneResult actionResult = await _collection.ReplaceOneAsync(x=>x.Id == id, item, new UpdateOptions { IsUpsert = true });
            return actionResult.IsAcknowledged && actionResult.ModifiedCount > 0;
        }

        public async Task<IEnumerable<T>> Find(Expression<Func<T,bool>> filter)
        {
            return await _collection.Find(filter).ToListAsync();
        }
    }
}