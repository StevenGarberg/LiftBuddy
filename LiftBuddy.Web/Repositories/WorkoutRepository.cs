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
                    Id = Guid.NewGuid(),
                    PerformedAt = DateTime.UtcNow
                });
            }
            return workouts;
        }
    }
}