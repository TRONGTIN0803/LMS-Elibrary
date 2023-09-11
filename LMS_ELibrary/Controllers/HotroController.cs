
using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_ELibrary.ServiceInterface;

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
    }
}
