using System;
using System.Collections.Generic;
using LiftBuddy.Models;

namespace LiftBuddy.Web.Repositories
{
    public class WorkoutRepository
    {
        public IList<Workout> GetAll()
        {
            var workouts = new List<Workout>();
            for (var i = 0; i < 10; i++)
            {
                workouts.Add(new Workout
                {
                    Id = Guid.NewGuid().ToString(),
                    PerformedAt = DateTime.UtcNow,
                    Exercises = new List<Exercise>
                    {
                        new StrengthExercise { Name = "Flat Bench" },
                        new StrengthExercise { Name = "Squats" }
                    }
                });
            }
            return workouts;
        }
    }
}