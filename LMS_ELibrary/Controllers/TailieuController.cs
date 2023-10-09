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

        [HttpGet("GetallBaigiang")]
        public async Task<IActionResult> getAll()
        {
            return Ok(await _tailieuService.GetAllBaigiang());  
        }

        [HttpGet("/Giangvien/Tailieucuatoi")]
        public async Task<IActionResult> Tailieucuatoi(int user_id)
        {
            return Ok(await _tailieuService.Tailieucuatoi(user_id));
        }

        [HttpGet("search-Baigiang")]
        public async Task<IActionResult> searchBaigiang(int user_id, string key)
        {
            return Ok(await _tailieuService.searchBaigiang(user_id, key));
        }

        [HttpPost("Themmoibaigiang")]
        public async Task<IActionResult> Themmoibaigiang(Taomoi_Baigiang_Request_DTO model)
        {
            return Ok(await _tailieuService.TaomoiBaigiang(model));
        }

        [HttpPost("Them_Tainguyen_cho_baigiang")]
        public async Task<IActionResult> Them_Tainguyen_cho_baigiang(Taotainguyen_Baigiang_Request_DTO model)
        {
            return Ok(await _tailieuService.Taotainguyen_cho_Baigiang(model));
        }

        [HttpPut("updateTailieu")]
        public async Task<IActionResult> updateTailieu(Edit_Baigiang_Tainguyen_Request_DTO model)
        {
            return Ok(await _tailieuService.editTailieu(model));
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

        [HttpDelete("delTailieu")]
        public async Task<IActionResult>delleteTailieu(Delete_Entity_Request_DTO model)
        {
            return Ok(await _tailieuService.delTailieu(model));
        }

        [HttpGet("XemBaigiangtheoTrangthai")]
        public async Task<IActionResult> XemBaigiangtheoTrangthai(int status)
        {
            return Ok(await _tailieuService.XemBaigiangtheoTrangthai(status));
        }

        [HttpGet("Xem_File_theo_Monhoc/test")]
        public async Task<IActionResult> Xem_File_theo_Monhoc(int monhoc_id,int option,int status)
        {
            return Ok(await _tailieuService.Xem_File_theo_Mon(monhoc_id,option,status));
        }

        [HttpPut("XetDuyetBaigiang")]
        public async Task<IActionResult> XetDuyetBaigiang(Xetduyet_Request_DTO model)
        {
            return Ok(await _tailieuService.XetDuyetBaigiang(model));
        }

        [HttpPut("XetDuyetFile")]
        public async Task<IActionResult> XetDuyetFile(Xetduyet_Request_DTO model)
        {
            return Ok(await _tailieuService.XetduyetFile(model));
        }
        [HttpGet("Top10-Filetailen-Gannhat")]
        public async Task<IActionResult>GetfileTop10(int user_id,int page)
        {
            return Ok(await _tailieuService.Top10_Filetailen_Gannhat(user_id,page));
        }
    }
}
