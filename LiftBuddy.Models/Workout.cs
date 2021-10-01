using System;
using System.Collections.Generic;

namespace LiftBuddy.Models
{
    public class Workout : BaseResource
    {
        public DateTime PerformedAt { get; set; } = DateTime.UtcNow;
        public int Duration { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        
        public Workout() { }
        public Workout(Routine routine)
        {
            Exercises = routine.Exercises;
        }
    }
}