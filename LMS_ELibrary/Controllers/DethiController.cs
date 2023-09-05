using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DethiController : ControllerBase
    {
        private readonly IDethiService _dethiService;
        public DethiController(IDethiService dethiService)
        {
            _dethiService = dethiService;
        }

        [HttpGet("getalldethi")]
        public async Task<IActionResult> getall()
        {
            return Ok(await _dethiService.getalldethi());
        }

        [HttpGet("FilterDethi_theo_Monhoc/{Monhoc_id}")]
        public async Task<IActionResult>filterDethi_Monhoc(int Monhoc_id)
        {
            return Ok(await _dethiService.filterDethitheoMon(Monhoc_id));
        }

        [HttpGet("FilterDethi_theo_Tomon/{Tomon_Id}")]
        public async Task<IActionResult> filterDethi_Tomon(int Tomon_Id)
        {
            return Ok(await _dethiService.filterDethitheoTohomon(Tomon_Id));
        }
    }
}
