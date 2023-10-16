using LMS_ELibrary.Model;
using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using LMS_ELibrary.ServiceInterface;
using LMS_ELibrary.Data;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class DethiController : ControllerBase
    {
        private readonly IDethiService _dethiService;
        public DethiController(IDethiService dethiService)
        {
            _dethiService = dethiService;
        }

        [HttpGet("getalldethi")]
        public async Task<IActionResult> getall(int user_id)
        {
            return Ok(await _dethiService.getalldethi(user_id));
        }

        [HttpGet("FilterDethi_theo_Monhoc/{Monhoc_id}")]
        public async Task<IActionResult>filterDethi_Monhoc(int Monhoc_id)
        {
            return Ok(await _dethiService.filterDethitheoMon(Monhoc_id));
        }

        [HttpGet("FilterDethi_theo_Tomon/{Tomon_Id}")]
        public async Task<IActionResult> filterDethi_Tomon(int Tomon_Id)
        {
            return Ok(await _dethiService.filterDethitheoTohomon(Tomon_Id));
        }

        [HttpPost("addDethi_ngan_hang_cau_hoi")]
        public async Task<IActionResult> adddethi_nganhangcauhoi(Tao_dethi_tu_nganhangcauhoi_Request_DTO model)
        {

            return Ok(await _dethiService.tao_dethi_nganhangcauhoi(model)); 
        }

        [HttpPost("addDethiTructiep")]
        public async Task<IActionResult> addDethitructiep(Tao_cauhoi_tructiep_Request_DTO model)
        {
            return Ok(await _dethiService.tao_dethi_tructiep(model));
        }

        [HttpGet("searchDethi/{madethi}")]
        public async Task<IActionResult>searchDethi(string madethi)
        {
            return Ok(await _dethiService.searchDethi(madethi));
        }

        [HttpGet("chitietDethi/{id_dethi}")]
        public async Task<IActionResult>detailDethi(int id_dethi)
        {
            return Ok(await _dethiService.chitietDethi(id_dethi));
        }

        [HttpPut("doiMaDeThi")]
        public async Task<IActionResult> changeExamCode(Edit_Baigiang_Tainguyen_Request_DTO model)
        {
            return Ok(await _dethiService.doiMadethi(model));
        }

        [HttpPut("guiPheDuyet-Huyyeucau")]
        public async Task<IActionResult>guiPheduyet(Guiyeucau_Huyyeucau_Monhoc_Request_DTO model)
        {
            return Ok(await _dethiService.guiPheduyet(model));
        }

        [HttpDelete("XoaDethi")]
        public async Task<IActionResult>deleteDethi(Delete_Entity_Request_DTO model)
        {
            return Ok(await _dethiService.deleteDethi(model));
        }

        [HttpPost("tai_len_Dethi")]
        public async Task<IActionResult>tailenDethi(int user_id, List<IFormFile> files)
        {
            return Ok(await _dethiService.tai_len_Dethi(user_id, files));
        }

        [HttpPut("them_File_vao_Dethi/{dethi_id}")]
        public async Task<IActionResult> themFilevaoDethi(int dethi_id, File_Dethi_Db file)
        {
            return Ok(await _dethiService.them_File_vao_Dethi(dethi_id, file));
        }

        [HttpGet("xemDeThiTheoTrangThai")]
        public async Task<IActionResult>xemDethitheotrangthai(int status)
        {
            return Ok(await _dethiService.xemDeThitheoTrangThai(status));
        }

        [HttpPut("xetDuyetDeThi")]
        public async Task<IActionResult>xetDuyetDeThi(Xetduyet_Request_DTO model)
        {
            return Ok(await _dethiService.xetDuyetDeThi(model));
        }
    }
}
