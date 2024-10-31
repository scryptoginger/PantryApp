using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace PantryAppAPI.Models
{
    public class PantryItem {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }
        public int Quantity  { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Notes { get; set; }
        public string Location { get; set; }
    }
}