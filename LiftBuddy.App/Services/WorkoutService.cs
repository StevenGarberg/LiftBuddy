using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiftBuddy.App.Repositories;
using LiftBuddy.Models;

namespace LiftBuddy.App.Services
{
    public class WorkoutService
    {
        private readonly WorkoutRepository _workoutRepository;
        
        public WorkoutService(WorkoutRepository workoutRepository)
        {
            _workoutRepository = workoutRepository;
        }
        
        public async Task<Workout> CreateAsync(string ownerId, Workout workout)
        {
            if (workout == null)
                throw new ArgumentNullException(nameof(workout));
            
            workout.OwnerId = ownerId;
            
            return await _workoutRepository.CreateAsync(workout);
        }

        public async Task<Workout> GetById(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Workout>> GetAllByOwnerId(string ownerId)
        {
            throw new NotImplementedException();
        }
    }
}