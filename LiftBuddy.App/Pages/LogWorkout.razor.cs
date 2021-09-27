using System;
using System.Threading.Tasks;
using LiftBuddy.App.Services;
using LiftBuddy.Models;
using LiftBuddy.Models.Enums;
using Microsoft.AspNetCore.Components;

namespace LiftBuddy.App.Pages
{
    public partial class LogWorkout
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private WorkoutService WorkoutService { get; set; }

        private string userId;
        private Workout workout;
    
        private bool loadFailed = false;
        private Exception exception;

        
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            userId = Guid.Empty.ToString();
            
            try
            {
                workout = new Workout
                {
                    OwnerId = userId
                };
            }
            catch(Exception e)
            {
                loadFailed = true;
                exception = e;
                NavigationManager.NavigateTo("/");
            }
        }
    
        private async Task HandleValidSubmit()
        {
            workout = await WorkoutService.CreateAsync(userId, workout);
        }
    
        private async Task Submit()
        {
            workout = await WorkoutService.CreateAsync(userId, workout);
            
            NavigationManager.NavigateTo("/");
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/");
        }

        private void AddExercise(ExerciseType type)
        {
            Exercise exercise;
            switch (type)
            {
                default:
                    return;
                case ExerciseType.Strength:
                    exercise = new StrengthExercise();
                    break;
                case ExerciseType.Cardio:
                    exercise = new CardioExercise();
                    break;
            }
            workout.Exercises.Add(exercise);
        }

        private void RemoveExercise(Exercise exercise)
        {
            workout.Exercises.Remove(exercise);
        }
    }
}