using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LiftBuddy.Models;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace LiftBuddy.App.Repositories
{
    public class RoutineRepository
    {
        private readonly IMongoClient _mongoClient;

        public RoutineRepository(IMongoClient mongoClient)
        {
            _mongoClient = mongoClient;
        }

        public async Task<Routine> CreateAsync(Routine workout)
        {
            workout.Id = Guid.NewGuid().ToString();
            workout.CreatedAt = DateTime.UtcNow;
            workout.UpdatedAt = workout.CreatedAt;
            await GetCollection().InsertOneAsync(workout);
            return workout;
        }
        
        public async Task<Routine> Update(string id, Routine data)
        {
            data.UpdatedAt = DateTime.UtcNow;
            await GetCollection().ReplaceOneAsync(x => x.Id == id, data);
            return data;
        }
        
        public async Task Delete(string id)
        {
            await GetCollection().UpdateOneAsync(x => x.Id == id,
                Builders<Routine>.Update.Set(x => x.Deleted, true));
        }

        public async Task<Routine> GetByIdAsync(string id)
        {
            return await GetCollection().AsQueryable()
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Routine>> GetAll()
        {
            return await GetCollection().AsQueryable()
                .ToListAsync();
        }
        
        public async Task<List<Routine>> GetAllByOwnerId(string ownerId)
        {
            return await GetCollection().AsQueryable()
                .Where(x => x.OwnerId == ownerId && !x.Deleted)
                .ToListAsync();
        }
        
        private IMongoCollection<Routine> GetCollection()
        {
            IMongoDatabase database = _mongoClient.GetDatabase("liftbuddy");
            IMongoCollection<Routine> collection = database.GetCollection<Routine>("routines");
            return collection;
        }
    }
}