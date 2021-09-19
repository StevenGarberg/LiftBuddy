using System.Text.Json.Serialization;

namespace LiftBuddy.Models
{
    public abstract class BaseResource
    {
        public string Id { get; set; }
        
        [JsonIgnore]
        public bool Deleted { get; set; }
    }
}