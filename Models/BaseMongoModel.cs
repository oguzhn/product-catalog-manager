using System;
using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes; 

namespace ProductCatalogManager.Models
{
    public class BaseMongoModel : BaseDbModel<string>
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonId]
        [BsonElement(Order = 0)]
        public string Id { get; set;} = ObjectId.GenerateNewId().ToString();

        [BsonRepresentation(BsonType.DateTime)]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        [BsonElement(Order = 101)]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow; 
    }
    
}