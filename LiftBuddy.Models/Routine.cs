using System.Collections.Generic;

namespace LiftBuddy.Models
{
    public class Routine : BaseResource
    {
        public string Name { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
        
        public Routine() { }
        public Routine(Workout workout)
        {
            Exercises = workout?.Exercises;
        }
    }
}