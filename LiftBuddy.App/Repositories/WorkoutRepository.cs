using System;
using System.Threading.Tasks;
using LiftBuddy.Models;
using MongoDB.Driver;

namespace LiftBuddy.App.Repositories
{
    public class WorkoutRepository
    {
        private readonly IMongoClient _mongoClient;

        public WorkoutRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        
        public async Task<Workout> CreateAsync(Workout workout)
        {
            workout.Id = Guid.NewGuid().ToString();
            workout.CreatedAt = DateTime.UtcNow;
            workout.UpdatedAt = workout.CreatedAt;
            await GetCollection().InsertOneAsync(workout);
            return workout;
        }
        
        private IMongoCollection<Workout> GetCollection()
        {
            IMongoDatabase database = _mongoClient.GetDatabase("liftbuddy");
            IMongoCollection<Workout> collection = database.GetCollection<Workout>("workouts");
            return collection;
        }
    }
}