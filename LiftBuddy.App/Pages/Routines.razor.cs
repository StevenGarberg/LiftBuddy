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
    public partial class Routines
    {
        [Inject] private NavigationManager NavigationManager { get; set; }
        [Inject] private DialogService  DialogService  { get; set; }
        [Inject] private NotificationService NotificationService { get; set; }
        [Inject] private RoutineService RoutineService { get; set; }

        private string userId;
        private List<Routine> routines;
        
        private RadzenGrid<Routine> grid;
    
        private bool loadFailed = false;
        private Exception exception;

        
        protected override async Task OnParametersSetAsync()
        {
            await base.OnParametersSetAsync();

            userId = Guid.Empty.ToString();
            
            try
            {
                routines = await RoutineService.GetAllByOwnerId(userId);
            }
            catch(Exception e)
            {
                loadFailed = true;
                exception = e;
                NavigationManager.NavigateTo("/");
            }
        }

        private async Task Delete(string id)
        {
            var confirmed = await DialogService.Confirm("Are you sure?", "Delete Routine",
                new ConfirmOptions {OkButtonText = "Yes", CancelButtonText = "No"});

            if (confirmed == true)
            {
                await RoutineService.Delete(id);
                
                routines.RemoveAll(a => a.Id == id);
                await grid.Reload();
                
                NotificationService.Notify(new NotificationMessage
                {
                    Summary = "Success!", Detail = "Routine was deleted.", Duration = 5000f, Severity = NotificationSeverity.Success
                });
            }
        }
    }
}