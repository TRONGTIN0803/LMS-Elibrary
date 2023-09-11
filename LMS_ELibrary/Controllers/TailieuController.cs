using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TailieuController : ControllerBase
    {
        private readonly TailieuService _tailieuService;
        public TailieuController(TailieuService tailieuService)
        {
            _tailieuService = tailieuService;
        }

        [HttpGet("getall/{user_id}")]
        public async Task<IActionResult> getalltailieu(int user_id)
        {
            return Ok(await _tailieuService.getAlltailieu(user_id));
        }

        [HttpPut("updateTailieu/{id}")]
        public async Task<IActionResult> updateTailieu(int id,Tailieu_Baigiang_Model tailieu)
        {
            return Ok(await _tailieuService.editTailieu(id,tailieu));
        }

        [HttpPost("addTailieu")]
        public async Task<IActionResult>addTailieu(Tailieu_Baigiang_Model tailieu)
        {
            return Ok(await _tailieuService.addTailieu(tailieu));
        }

        [HttpDelete("delTailieu/{id}")]
        public async Task<IActionResult>delleteTailieu(int id)
        {
            return Ok(await _tailieuService.delTailieu(id));
        }
    }
}
