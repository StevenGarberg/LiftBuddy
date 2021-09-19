using System;
using System.Collections.Generic;
using System.Diagnostics;
using LiftBuddy.Models;
using LiftBuddy.Web.Models;
using LiftBuddy.Web.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LiftBuddy.Web.Controllers
{
    [Route("workouts")]
    public class WorkoutController : Controller
    {
        private readonly ILogger<WorkoutController> _logger;
        private readonly WorkoutRepository _repository;

        public WorkoutController(ILogger<WorkoutController> logger, WorkoutRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }
        
        public IActionResult Index()
        {
            var workouts = _repository.GetAll();
            return View(workouts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}