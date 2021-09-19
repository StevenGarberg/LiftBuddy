using System;

namespace LiftBuddy.Models
{
    public class CardioExercise : Exercise
    {
        public int Duration { get; set; }
        public int CaloriesBurned { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }
}