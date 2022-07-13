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

        [HttpPost("/tank-right")]
        public async Task<ActionResult<string>> MoveTankRight(MoveTankModel model)
        {
            return Ok(await _tankService.MoveTankRight(model));
        }

        [HttpPost("/tank-up")]
        public async Task<ActionResult<string>> MoveTankUp(MoveTankModel model)
        {
            return Ok(await _tankService.MoveTankUp(model));
        }

        [HttpPost("/tank-down")]
        public async Task<ActionResult<string>> MoveTankDown(MoveTankModel model)
        {
            return Ok(await _tankService.MoveTankDown(model));
        }

        [HttpPost("/tank-rotate-left")]
        public async Task<ActionResult<string>> RotateTankLeft(MoveTankModel model)
        {
            return Ok(await _tankService.RotateTankLeft(model));
        }

        [HttpPost("/tank-rotate-right")]
        public async Task<ActionResult<string>> RotateTankRight(MoveTankModel model)
        {
            return Ok(await _tankService.RotateTankRight(model));
        }
    }
}
