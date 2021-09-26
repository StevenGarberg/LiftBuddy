using System;
using System.Threading.Tasks;
using LiftBuddy.Models;

namespace LiftBuddy.App.Repositories
{
    public class WorkoutRepository
    {
        public async Task<Workout> CreateAsync(Workout workout)
        {
            workout.CreatedAt = DateTime.UtcNow;
            workout.UpdatedAt = workout.CreatedAt;
            // TODO: Insert into database
            return await Task.FromResult(workout);
        }
    }
}