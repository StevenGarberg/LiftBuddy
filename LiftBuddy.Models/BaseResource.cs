using System;
using System.Text.Json.Serialization;
using MongoDB.Bson.Serialization.Attributes;

namespace LiftBuddy.Models
{
    public abstract class BaseResource
    {
        [BsonId]
        public string Id { get; set; }

        public string OwnerId { get; set; }

        public int Verison { get; set; } = 1;
        
        [JsonIgnore]
        public bool Deleted { get; set; }
        
        public DateTime CreatedAt { get; set; }
        
        public DateTime UpdatedAt { get; set; }
    }
}