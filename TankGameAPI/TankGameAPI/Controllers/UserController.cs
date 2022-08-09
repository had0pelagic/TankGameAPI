using Microsoft.AspNetCore.Mvc;
using TankGameAPI.Models.User;
using TankGameAPI.Services;

namespace TankGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/user")]
        public async Task<ActionResult> CreateUser(CreateUserModel model)
        {
            return Ok(await _userService.CreateUser(model));
        }

        /// <summary>
        /// Removes existing user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/user-remove")]
        public async Task<ActionResult> RemoveUser(UserModel model)
        {
            return Ok(await _userService.RemoveUser(model));
        }

        /// <summary>
        /// Checks if user exists by given username
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/user-valid")]
        public async Task<ActionResult> IsUserValid(UserModel model)
        {
            return Ok(await _userService.IsUserValid(model));
        }
    }
}
