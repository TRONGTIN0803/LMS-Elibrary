using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TobomonController : ControllerBase
    {
        public readonly ITobomonService _tobomonService;
        public TobomonController(ITobomonService tobomonService)
        {
            _tobomonService = tobomonService;
        }

        [HttpGet("Getall")]
        public async Task<IActionResult> Getall()
        {
            return Ok(await _tobomonService.Getall());
        }

        [HttpPost("AddTobomon")]
        public async Task<IActionResult>AddTobomon(Tobomon_Request_DTO model)
        {
            return Ok(await _tobomonService.AddTobomon(model));
        }
        [HttpPut("EditTobomon")]
        public async Task<IActionResult> EditTobomon(Tobomon_Request_DTO model)
        {
            return Ok(await _tobomonService.editTobomon(model));
        }
        [HttpDelete("DeleteTobomon")]
        public async Task<IActionResult> DeleteTobomon(Tobomon_Request_DTO model)
        {
            return Ok(await _tobomonService.deleteTobomon(model));
        }

        [HttpGet("List_Tobomon_Giangvien")]
        public async Task<IActionResult> List_Tobomon_Giangvien(int giangvien_Id)
        {
            return Ok(await _tobomonService.Xem_list_Tobomon_Giangvien(giangvien_Id));
        }
    }
}
