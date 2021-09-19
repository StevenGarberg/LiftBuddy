using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using LiftBuddy.Models;
using LiftBuddy.Models.Requests;
using LiftBuddy.Web.Clients;
using LiftBuddy.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LiftBuddy.Web.Controllers
{
    [Route("users")]
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IClient<User> _client;

        public UserController(ILogger<UserController> logger, IClient<User> client)
        {
            _logger = logger;
            _client = client;
        }
        
        public async Task<IActionResult> Index()
        {
            var users = await _client.GetAll() ?? new List<User>();
            return View(users);
        }
        
        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            var user = new UserCreateRequest();
            return View(user);
        }
        
        [HttpPost("create")]
        public async Task<IActionResult> CreatePost(UserCreateRequest request)
        {
            var user = await _client.Create(request);
            return RedirectToAction("Index");
        }
        
        [HttpGet("edit/{id}")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _client.Get(id);
            return View(user);
        }
        
        [HttpPost("edit")]
        public async Task<IActionResult> EditPost(User user)
        {
            var request = new UserUpdateRequest
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            };
            user = await _client.Update(user.Id, request);
            return RedirectToAction("Index");
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _client.Delete(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}