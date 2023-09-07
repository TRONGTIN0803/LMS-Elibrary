using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThongbaoController : ControllerBase
    {
        private readonly IThongbaoService _thongbaoService;

        public ThongbaoController(IThongbaoService thongbaoService)
        {
            _thongbaoService = thongbaoService;
        }

        [HttpGet("getAllThongbao")]
        public async Task<IActionResult>getall(int id)
        {
            return Ok(await _thongbaoService.getallThongbao(id));
        }

        [HttpGet("searchThongbao/{keyword}")]
        public async Task<IActionResult>searchThongbao(int user_id,string keyword)
        {
            return Ok(await _thongbaoService.searchThongbao(user_id, keyword));
        }
    }
}
