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

        [HttpPut("edit")]
        public async Task<IActionResult>editChudu(Edit_Baigiang_Tainguyen_Request_DTO model)
        {
            return Ok(await _chudeService.editChude(model));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> deleteChude(Delete_Entity_Request_DTO model)
        {
            return Ok(await _chudeService.deletetChude(model));
        }

        [HttpGet("Xem_Chude_trong_Monhoc")]
        public async Task<IActionResult> Xem_Chude_trong_Monhoc(int monhoc_id)
        {
            return Ok(await _chudeService.Xem_Chude_Monhoc(monhoc_id));
        }

    }
}
