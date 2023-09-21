using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_ELibrary.ServiceInterface;
using LMS_ELibrary.Model;

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

        [HttpGet("ChitietThongbao/{idthongbao}")]
        public async Task<IActionResult>chitietThongbao(int idthongbao)
        {
            return Ok(await _thongbaoService.chitietThongbao(idthongbao));
        }

        [HttpDelete("xoaThongbao")]
        public async Task<IActionResult> deleteThongbao([FromQuery]List<int> listIdthongbai)
        {
            return Ok(await _thongbaoService.xoaThongbao(listIdthongbai));
        }

        [HttpGet("Locthongbao/{status}")]
        public async Task<IActionResult>locThongbao(int user_id,int status)
        {
            return Ok(await _thongbaoService.locThongBao(user_id, status));
        }

        [HttpPut("danhDauThongBao/{thongbao_id}")]
        public async Task<IActionResult> danhDauThongBao(int thongbao_id,int status)
        {
            return Ok(await _thongbaoService.danhDauThongBao(thongbao_id,status));
        }
        //[HttpPost("Taothongbao")]
        //public async Task<IActionResult>Taothongbao(Thongbao_Model model)
        //{
        //    return Ok(await _thongbaoService.Taothongbao(model));
        //}
    }
}
