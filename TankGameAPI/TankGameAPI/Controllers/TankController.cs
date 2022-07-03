using Microsoft.AspNetCore.Mvc;
using TankGameAPI.Models.Tank;
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

        [HttpPost("/tank")]
        public async Task<ActionResult<string>> CreateTank(CreateTankModel model)
        {
            return Ok(await _tankService.CreateTank(model));
        }

        [HttpPost("/tank-left")]
        public async Task<ActionResult<string>> MoveTankLeft(MoveTankModel model)
        {
            return Ok(await _tankService.MoveTankLeft(model));
        }
    }
}
