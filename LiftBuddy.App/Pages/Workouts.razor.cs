using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LiftBuddy.App.Services;
using LiftBuddy.Models;
using LiftBuddy.Models.Enums;
using Microsoft.AspNetCore.Components;
using Radzen;
using Radzen.Blazor;

namespace LiftBuddy.App.Pages
{
    public partial class Workouts
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private DialogService  DialogService  { get; set; }
        [Inject] private NotificationService NotificationService { get; set; }
        [Inject] private WorkoutService WorkoutService { get; set; }

        private string userId;
        private List<Workout> workouts;
        
        private RadzenGrid<Workout> grid;
    
        private bool loadFailed = false;
        private Exception exception;

        
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            userId = Guid.Empty.ToString();
            
            try
            {
                workouts = await WorkoutService.GetAllByOwnerId(userId);
            }
            catch(Exception e)
            {
                loadFailed = true;
                exception = e;
                NavigationManager.NavigateTo("/");
            }
        }
    
        private void Create()
        {
            NavigationManager.NavigateTo("/log");
        }
        
        private void Edit(string id)
        {
            
            NavigationManager.NavigateTo($"/log/{id}");
        }
        
        private async Task Delete(string id)
        {
            var confirmed = await DialogService.Confirm("Are you sure?", "Delete Workout",
                new ConfirmOptions {OkButtonText = "Yes", CancelButtonText = "No"});

            if (confirmed == true)
            {
                await WorkoutService.Delete(id);
                
                workouts.RemoveAll(a => a.Id == id);
                await grid.Reload();
                
                NotificationService.Notify(new NotificationMessage
                {
                    Summary = "Success!", Detail = "Workout was deleted.", Duration = 5000f, Severity = NotificationSeverity.Success
                });
            }
        }
    }
}