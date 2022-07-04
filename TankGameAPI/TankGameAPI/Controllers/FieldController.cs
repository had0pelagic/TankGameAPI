using Microsoft.AspNetCore.Mvc;
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
        public async Task<ActionResult<string>> GetField()
        {
            return Ok(await _fieldService.GetField());
        }
    }
}
