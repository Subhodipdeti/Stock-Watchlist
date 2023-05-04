using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using StockWatchlist.Models;

namespace StockWatchlist.Services
{
    public class MongoDBService
    {
        private readonly IMongoCollection<Watchlist> _watchlistCollection;
        private readonly IMongoCollection<AddWatchlist> _addwatchlistCollection;

        public MongoDBService(IOptions<MongoDBSettings> mongoDBSettings)
        {
            MongoClient client = new MongoClient(mongoDBSettings.Value.ConnectionURI);
            IMongoDatabase database = client.GetDatabase(mongoDBSettings.Value.DatabaseName);
            _watchlistCollection = database.GetCollection<Watchlist>(mongoDBSettings.Value.CollectionName);
            _addwatchlistCollection = database.GetCollection<AddWatchlist>(mongoDBSettings.Value.CollectionName);
            
        }

        public async Task CreateAsync(AddWatchlist watchlist) {
          await _addwatchlistCollection.InsertOneAsync(watchlist);
          return;
        }

        public async Task<List<Watchlist>> GetAsync() {
            return await _watchlistCollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task AddToWatchlistAsync(string id, string name) {
            FilterDefinition<Watchlist> filter = Builders<Watchlist>.Filter.Eq("Id", id);
            UpdateDefinition<Watchlist> update = Builders<Watchlist>.Update.AddToSet<string>("Name", name);
            await _watchlistCollection.UpdateOneAsync(filter, update);
            return;
        }

        public async Task DeleteAsync(string id) {
            FilterDefinition<Watchlist> filter = Builders<Watchlist>.Filter.Eq("Id", id);
            await _watchlistCollection.DeleteManyAsync(filter);
            return;
        }
    }
}