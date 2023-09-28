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

        [HttpGet("Getall")]
        public async Task<IActionResult> getAll()
        {
            return Ok(await _tailieuService.GetAlltailieu());  
        }

        [HttpGet("/Giangvien/Tailieucuatoi")]
        public async Task<IActionResult> Tailieucuatoi(int user_id)
        {
            return Ok(await _tailieuService.Tailieucuatoi(user_id));
        }

        [HttpGet("search/{key}")]
        public async Task<IActionResult> searchBaigiang(int user_id, string key)
        {
            return Ok(await _tailieuService.searchBaigiang(user_id, key));
        }

        [HttpGet("TailieuMonhoc")]
        public async Task<IActionResult> listTailieuMonhoc(int monId, string? status, string? type)
        {
            if(status!=null && type != null){
                return Ok(await _tailieuService.list_Tailieu_Monhoc(monId, status, type));
            }
            else if(status==null && type == null)
            {
                return Ok(await _tailieuService.list_Tailieu_Monhoc(monId));
            }else if (status!=null)
            {
                return Ok(await _tailieuService.list_Tailieu_Monhoc_status(monId, status));
            }
            else if (type != null)
            {
                return Ok(await _tailieuService.list_Tailieu_Monhoc(monId, type));
            }
            else
            {
                return BadRequest();
            }
            
        }

        [HttpPut("updateTailieu/{id}")]
        public async Task<IActionResult> updateTailieu(int id,Tailieu_Baigiang_Model tailieu)
        {
            return Ok(await _tailieuService.editTailieu(id,tailieu));
        }

        [HttpPost("tai_len_Tai_Nguyen")]
        public async Task<IActionResult> tai_len_Tai_Lieu(int user_id, List<IFormFile> files)
        {
            return Ok(await _tailieuService.tai_len_Tai_Nguyen(user_id, files));
        }

        [HttpPost("tai_len_Bai_Giang")]
        public async Task<IActionResult> tai_len_Bai_Giang(int user_id, List<IFormFile> files)
        {
            return Ok(await _tailieuService.tai_len_Bai_Giang(user_id, files));
        }

        [HttpPut("them_vao_Monhoc_va_Chude")]
        public async Task<IActionResult>themvaoMonhocvaChude(Gui_pheduyet_tailieu_Request_DTO model)
        {
            return Ok(await _tailieuService.them_vao_Monhoc_va_Chude(model));
        }

        [HttpDelete("delTailieu/{id}")]
        public async Task<IActionResult>delleteTailieu(int id)
        {
            return Ok(await _tailieuService.delTailieu(id));
        }

        [HttpGet("XemTailieutheoTrangthai")]
        public async Task<IActionResult>xemTaiLieuTheoTrangThai(int status)
        {
            return Ok(await _tailieuService.XemTailieutheoTrangthai(status));
        }

        [HttpPut("XetDuyetTaiLieu")]
        public async Task<IActionResult> xetDuyetTaiLieu(Xetduyet_Request_DTO model)
        {
            return Ok(await _tailieuService.XetDuyetTaiLieu(model));
        }

    }
}
