using LMS_ELibrary.Data;
using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaigiangController : ControllerBase
    {
        private readonly IBaigiangService _baigiangService;
        public BaigiangController(IBaigiangService baigiangService)
        {
            _baigiangService = baigiangService;
        }

        [HttpGet("getall/{user_id}")]
        public async Task<IActionResult>getallbaigigang(int user_id)
        {
            return Ok(await _baigiangService.getallbaigigang(user_id));
        }

        [HttpGet("search/{key}")]
        public async Task<IActionResult>searchBaigiang(int user_id,string key)
        {
            return Ok(await _baigiangService.searchBaigiang(user_id,key));
        }

        [HttpGet("Filter/{Monhoc_Id}")]
        public async Task<IActionResult>filterBaigiang(int user_id,int Monhoc_Id)
        {
            return Ok(await _baigiangService.filterBaigiang(user_id,Monhoc_Id));
        }

        [HttpPost("addBaiGiang")]
        public async Task<IActionResult> addBaiGiang_Chude(Tailieu_Baigiang_Db baigiang)
        {
            return Ok(await _baigiangService.addBaigiang(baigiang));
        }

        [HttpPut("themvaoMonhoc")]
        public async Task<IActionResult>themvaomonhoc(int iddoc, int idmon)
        {
            return Ok(await _baigiangService.changeMonhoc(iddoc, idmon));
        }

        [HttpDelete("XoaBaigiang")]
        public async Task<IActionResult>xoaBaigiang(int user_id,int baigiang_id)
        {
            return Ok(await _baigiangService.XoaBaigiang(user_id, baigiang_id));
        }
    }
}
