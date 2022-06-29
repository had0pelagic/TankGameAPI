using Microsoft.AspNetCore.Mvc;
using TankGameAPI.Models;
using TankGameAPI.Services;

namespace TankGameAPI.Controllers
{
    [ApiController]
    [Route("/api")]
    public class UserController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Route("/create-user")]
        public async Task<ActionResult> CreateUser(CreateUserModel model)
        {
            throw new NotImplementedException();
        }
    }
}
