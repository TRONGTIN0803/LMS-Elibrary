using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotroController : ControllerBase
    {
        private readonly HotroService _hotroService;

        public HotroController(HotroService hotroService)
        {
            _hotroService = hotroService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> getall()
        {
            return Ok(await _hotroService.getAlllisthotro());
        }
    }
}
