using Microsoft.AspNetCore.Mvc;
using TankGameAPI.Models;
using TankGameAPI.Services;

namespace TankGameAPI.Controllers
{
    [ApiController]
    [Route("/api")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("/user")]
        public async Task<ActionResult> CreateUser(CreateUserModel model)
        {
            return Ok(await _userService.CreateUser(model));
        }

        [HttpGet("/get-user")]
        public async Task<ActionResult> GetUser()
        {
            return Ok(await _userService.GetUser());
        }
    }
}
