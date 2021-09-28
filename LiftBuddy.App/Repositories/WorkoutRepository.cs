using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiftBuddy.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

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
        
        public async Task<Workout> Update(string id, Workout data)
        {
            data.UpdatedAt = DateTime.UtcNow;
            await GetCollection().ReplaceOneAsync(x => x.Id == id, data);
            return data;
        }
        
        public async Task Delete(string id)
        {
            await GetCollection().UpdateOneAsync(x => x.Id == id,
                Builders<Workout>.Update.Set(x => x.Deleted, true));
        }

        public async Task<Workout> GetByIdAsync(string id)
        {
            return await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<List<Workout>> GetAll()
        {
            return await GetCollection().AsQueryable()
                .ToListAsync();
        }
        
        public async Task<List<Workout>> GetAllByOwnerId(string ownerId)
        {
            return await GetCollection().AsQueryable()
                .Where(w => w.OwnerId == ownerId && !w.Deleted)
                .ToListAsync();
        }
        
        private IMongoCollection<Workout> GetCollection()
        {
            IMongoDatabase database = _mongoClient.GetDatabase("liftbuddy");
            IMongoCollection<Workout> collection = database.GetCollection<Workout>("workouts");
            return collection;
        }
    }
}