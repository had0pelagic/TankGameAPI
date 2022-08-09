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

        /// <summary>
        /// Creates a tank with random coordinates
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/tank")]
        public async Task<ActionResult<string>> CreateTank(CreateTankModel model)
        {
            return Ok(await _tankService.CreateTank(model));
        }

        /// <summary>
        /// Moves tank left by 1 pixel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/tank-left")]
        public async Task<ActionResult<string>> MoveTankLeft(MoveTankModel model)
        {
            return Ok(await _tankService.MoveTankLeft(model));
        }

        /// <summary>
        /// Moves tank right by 1 pixel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/tank-right")]
        public async Task<ActionResult<string>> MoveTankRight(MoveTankModel model)
        {
            return Ok(await _tankService.MoveTankRight(model));
        }

        /// <summary>
        /// Moves tank up by 1 pixel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/tank-up")]
        public async Task<ActionResult<string>> MoveTankUp(MoveTankModel model)
        {
            return Ok(await _tankService.MoveTankUp(model));
        }

        /// <summary>
        /// Moves tank down by 1 pixel
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/tank-down")]
        public async Task<ActionResult<string>> MoveTankDown(MoveTankModel model)
        {
            return Ok(await _tankService.MoveTankDown(model));
        }

        /// <summary>
        /// Rotates tank left by 90 degrees
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/tank-rotate-left")]
        public async Task<ActionResult<string>> RotateTankLeft(MoveTankModel model)
        {
            return Ok(await _tankService.RotateTankLeft(model));
        }

        /// <summary>
        /// Rotates tank right by 90 degrees
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/tank-rotate-right")]
        public async Task<ActionResult<string>> RotateTankRight(MoveTankModel model)
        {
            return Ok(await _tankService.RotateTankRight(model));
        }

        /// <summary>
        /// Signal to make a tank shoot
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("/tank-attack")]
        public async Task<ActionResult<TankAttackModel>> AttackTank(MoveTankModel model)
        {
            return Ok(await _tankService.Attack(model));
        }
    }
}
