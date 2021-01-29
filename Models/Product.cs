using System;
using MongoDB.Bson;  
using MongoDB.Bson.Serialization.Attributes; 

namespace ProductCatalogManager.Models
{
    public class Product : BaseMongoModel
    {
        [BsonElement("Code")]
        public string Code{ get; set; }  

        [BsonElement("Name")]
        public string Name{ get; set; }

        [BsonElement("Photo")]
        public string Photo{ get; set; }

        [BsonElement("Price")]
        public int Price {get;set;}

        [BsonElement("LastUpdated")]
        public DateTime LastUpdated{get;set;}

        [BsonElement("IsConfirmed")]
        public bool IsConfirmed {get;set;}

    }
}