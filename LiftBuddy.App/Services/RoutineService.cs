using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiftBuddy.App.Repositories;
using LiftBuddy.Models;

namespace LiftBuddy.App.Services
{
    public class RoutineService
    {
        private readonly RoutineRepository _routineRepository;
        
        public RoutineService(RoutineRepository routineRepository)
        {
            _routineRepository = routineRepository;
        }
        
        public async Task<Routine> CreateAsync(string ownerId, Routine routine)
        {
            if (routine == null)
                throw new ArgumentNullException(nameof(routine));
            
            routine.OwnerId = ownerId;
            
            return await _routineRepository.CreateAsync(routine);
        }

        public async Task<Routine> Update(string id, Routine routine)
        {
            return await _routineRepository.Update(id, routine);
        }
        
        public async Task Delete(string id)
        {
            await _routineRepository.Delete(id);
        }

        public async Task<Routine> GetById(string id)
        {
            return await _routineRepository.GetByIdAsync(id);
        }

        public async Task<List<Routine>> GetAllByOwnerId(string ownerId)
        {
            var routines = await _routineRepository.GetAllByOwnerId(ownerId);
            return routines?.OrderBy(x => x.Name).ToList();
        }

        public async Task<List<Routine>> GetAll()
        {
            var routines = await _routineRepository.GetAll();
            return routines?.OrderBy(x => x.Name).ToList();
        }
    }
}