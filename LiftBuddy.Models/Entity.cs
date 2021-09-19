using System;

namespace LiftBuddy.Models
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public int Version { get; set; }
        public bool Deleted { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}