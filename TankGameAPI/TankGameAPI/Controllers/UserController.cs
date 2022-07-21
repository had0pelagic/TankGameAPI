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

        [HttpPost("/user-remove")]
        public async Task<ActionResult> RemoveUser(UserModel model)
        {
            return Ok(await _userService.RemoveUser(model));
        }

        [HttpPost("/user-valid")]
        public async Task<ActionResult> IsUserValid(UserModel model)
        {
            return Ok(await _userService.IsUserValid(model));
        }
    }
}
