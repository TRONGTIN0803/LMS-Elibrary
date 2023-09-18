using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_ELibrary.ServiceInterface;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TailieuController : ControllerBase
    {
        private readonly ITailieuService _tailieuService;
        public TailieuController(ITailieuService tailieuService)
        {
            _tailieuService = tailieuService;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> getalltailieu([FromQuery]int user_id)
        {
            return Ok(await _tailieuService.getAlltailieu(user_id));
        }

        [HttpPut("updateTailieu/{id}")]
        public async Task<IActionResult> updateTailieu(int id,Tailieu_Baigiang_Model tailieu)
        {
            return Ok(await _tailieuService.editTailieu(id,tailieu));
        }

        [HttpPost("tai_len_Tai_Lieu")]
        public async Task<IActionResult> tai_len_Tai_Lieu(int user_id, List<IFormFile> files)
        {
            return Ok(await _tailieuService.tai_len_Tai_Lieu(user_id, files));
        }

        [HttpPut("them_vao_Monhoc_va_Chude")]
        public async Task<IActionResult>themvaoMonhocvaChude(int monhoc_id,int chude_id, List<int> tailieu_id)
        {
            return Ok(await _tailieuService.them_vao_Monhoc_va_Chude(monhoc_id, chude_id, tailieu_id));
        }

        [HttpDelete("delTailieu/{id}")]
        public async Task<IActionResult>delleteTailieu(int id)
        {
            return Ok(await _tailieuService.delTailieu(id));
        }

        [HttpPost("TestFileBody")]
        public async Task<IActionResult>testne(Uptailieu_DTOcs tailieu, IFormFile file)
        {
            return Ok(await _tailieuService.TestUpfile(tailieu,file));
        }
    }
}
