using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace HtHInAction.Models
{
    public class BaseItem
    {
        [BsonId]
        public ObjectId Id { get; set; }
    }
}