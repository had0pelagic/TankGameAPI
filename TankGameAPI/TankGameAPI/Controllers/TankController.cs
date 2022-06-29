using Microsoft.AspNetCore.Mvc;
using TankGameAPI.Services;

namespace TankGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TankController : ControllerBase
    {
        private readonly ITankService _tankService;

        public TankController(ITankService tankService)
        {
            _tankService = tankService;
        }

        [HttpGet("/test")]
        public async Task<ActionResult<string>> Test()
        {
            return Ok("test");
        }
    }
}
