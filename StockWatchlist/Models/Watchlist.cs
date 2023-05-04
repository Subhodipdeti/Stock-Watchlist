
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace StockWatchlist.Models
{
    public class Watchlist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int price { get; set; }
    }
}