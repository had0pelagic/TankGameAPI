using Microsoft.AspNetCore.Mvc;
using TankGameAPI.Models.Field;
using TankGameAPI.Services;

namespace TankGameAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FieldController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet("/field")]
        public async Task<ActionResult<FieldModel>> GetField()
        {
            return Ok(await _fieldService.GetField());
        }
    }
}
