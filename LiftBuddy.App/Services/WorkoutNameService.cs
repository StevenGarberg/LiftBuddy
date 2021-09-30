using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using LiftBuddy.Models;

namespace LiftBuddy.App.Services
{
    public class WorkoutNameService
    {
        private readonly IReadOnlyCollection<NamedWorkout> _names;
        
        public WorkoutNameService()
        {
            var json = File.ReadAllText(@$"{AppDomain.CurrentDomain.BaseDirectory}\Data\workoutNames.json");
            var names = JsonSerializer.Deserialize<string[]>(json);
            _names = names?.Distinct().OrderBy(x => x).Select(x => new NamedWorkout(x)).ToArray();
        }

        public IReadOnlyCollection<NamedWorkout> GetAll()
        {
            return _names;
        }
    }
}