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

        public async Task<Workout> Update(string id, Workout workout)
        {
            return await _workoutRepository.Update(id, workout);
        }
        
        public async Task Delete(string id)
        {
            await _workoutRepository.Delete(id);
        }

        public async Task<Workout> GetById(string id)
        {
            return await _workoutRepository.GetByIdAsync(id);
        }

        public async Task<List<Workout>> GetAllByOwnerId(string ownerId)
        {
            return await _workoutRepository.GetAllByOwnerId(ownerId);
        }

        public async Task<List<Workout>> GetAll()
        {
            return await _workoutRepository.GetAll();
        }
    }
}