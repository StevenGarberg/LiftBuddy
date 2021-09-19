using System;
using System.Collections.Generic;

namespace LiftBuddy.Models
{
    public class Workout : Entity
    {
        public DateTime PerformedAt { get; set; }
        public List<Exercise> Exercises { get; set; } = new List<Exercise>();
    }
}