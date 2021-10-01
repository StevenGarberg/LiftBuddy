using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiftBuddy.App.Services;
using LiftBuddy.Models;
using LiftBuddy.Models.Enums;
using Microsoft.AspNetCore.Components;
using Radzen;

namespace LiftBuddy.App.Pages
{
    public partial class LogWorkout
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private DialogService  DialogService  { get; set; }
        [Inject] private NotificationService NotificationService { get; set; }
        [Inject] private RoutineService RoutineService { get; set; }
        [Inject] private WorkoutService WorkoutService { get; set; }
        [Inject] private WorkoutNameService WorkoutNameService { get; set; }
        
        [Parameter] public string Id { get; set; }

        private string userId;
        private Workout workout;
        private IReadOnlyCollection<NamedWorkout> namedWorkouts;

        private string routineId;
        private Routine routine;
        private List<Routine> routines;
    
        private bool loadFailed = false;
        private Exception exception;

        
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            userId = Guid.Empty.ToString();
            
            try
            {
                if (Id != null)
                {
                    workout = await WorkoutService.GetById(Id);
                    if (workout == null)
                    {
                        NavigationManager.NavigateTo("/workouts");
                    }
                }
                else
                {
                    routines = await  RoutineService.GetAllByOwnerId(userId);
                    workout = new Workout
                    {
                        OwnerId = userId
                    };
                }

                namedWorkouts = WorkoutNameService.GetAll();
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
            if (Id != null)
            {
                workout = await WorkoutService.Update(Id, workout);   
            }
            else
            {
                workout = await WorkoutService.CreateAsync(userId, workout);   
            }

            NavigationManager.NavigateTo("/workouts");
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("/workouts");
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

        private async Task RemoveExercise(Exercise exercise)
        {
            var confirmed = await DialogService.Confirm("Are you sure?", "Delete Exercise",
                new ConfirmOptions {OkButtonText = "Yes", CancelButtonText = "No"});
            
            workout.Exercises.Remove(exercise);
            
            NotificationService.Notify(new NotificationMessage
            {
                Summary = "Success!", Detail = $"\"{exercise.Name}\" was deleted.", Duration = 5000f, Severity = NotificationSeverity.Success
            });
        }

        private void MoveDown(int index, List<Exercise> exercises)
        {
            var exercise = exercises[index];
            var tempExercise = exercises[index + 1];
            exercises[index + 1] = exercise;
            exercises[index] = tempExercise;

        }

        private void MoveUp(int index, List<Exercise> exercises)
        {
            var exercise = exercises[index];
            var tempExercise = exercises[index - 1];
            exercises[index - 1] = exercise;
            exercises[index] = tempExercise;
        }

        private void UpdateName(object value, Exercise exercise)
        {
            exercise.Name = value.ToString();
        }

        private void ImportRoutine()
        {
            foreach (var exercise in routine.Exercises)
            {
                workout.Exercises.Add(exercise.Clone());
            }
        }

        private async Task SaveAsRoutine()
        {
            if (workout.Exercises == null || !workout.Exercises.Any())
                return;

            var firstExerciseName = workout.Exercises.First().GetType()
                .Name.Replace("Exercise", string.Empty);
            
            var newRoutine = new Routine(workout)
            {
                Name = $"{firstExerciseName} Routine"
            };
            
            newRoutine = await RoutineService.CreateAsync(userId, newRoutine);
        }
    }
}
