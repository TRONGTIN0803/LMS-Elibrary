using LMS_ELibrary.Data;
using LMS_ELibrary.Model;
using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_ELibrary.ServiceInterface;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChudeController : ControllerBase
    {
        private readonly IChudeService _chudeService;
        public ChudeController(IChudeService chudeService)
        {
            _chudeService = chudeService;
        }
        [HttpGet("Getall/{monhoc_id}")]
        public async Task<IActionResult> getAllChude(int monhoc_id)
        {
            return Ok(await _chudeService.getAllchude(monhoc_id));
        }

        [HttpPost("add")]
        public async Task<IActionResult>addChude(Chude_Model chude)
        {
            return Ok(await _chudeService.addChude(chude));
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult>editChudu(int id,Chude_Model chude)
        {
            return Ok(await _chudeService.editChude(id,chude));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deleteChude(int id)
        {
            return Ok(await _chudeService.deletetChude(id));
        }

       

    }
}
