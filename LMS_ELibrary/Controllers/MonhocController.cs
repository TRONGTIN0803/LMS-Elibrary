using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonhocController : ControllerBase
    {
        private readonly IMonhocService _monhocService;
        public MonhocController(IMonhocService monhocService)
        {
            _monhocService = monhocService;
        }

        [HttpGet("getAllMonhoc")]
        public async Task<IActionResult> getAllMonhoc()
        {
            var result=await _monhocService.getAllMonhoc();
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> searchMonhoc(string key)
        {
            var result =await _monhocService.searchMonhoc(key);
            return Ok(result);
        }

        [HttpGet("Loc")]//0=>theo ten ; 1=>theo lan truy cap gan nhat
        public async Task<IActionResult>locMonhoc(int sort)
        {
            return Ok(await _monhocService.locMonhoc(sort));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>xemChiTietMonHoc(int id)
        {
            var result =await  _monhocService.chitietMonhoc(id);
            return Ok(result);
        }
    }
}
