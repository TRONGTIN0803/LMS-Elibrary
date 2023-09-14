
using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_ELibrary.ServiceInterface;
using LMS_ELibrary.Model;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotroController : ControllerBase
    {
        private readonly IHotroService _hotroService;

        public HotroController(IHotroService hotroService)
        {
            _hotroService = hotroService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> getall()
        {
            return Ok(await _hotroService.getAlllisthotro());
        }

        [HttpPost("addHelp")]
        public async Task<IActionResult>addHelp(Help_Model help)
        {
            return Ok(await _hotroService.addHelp(help));
        }
    }
}
