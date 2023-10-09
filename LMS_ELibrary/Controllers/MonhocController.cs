using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_ELibrary.ServiceInterface;
using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;

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
        public async Task<IActionResult> getAllMonhoc(int userid,int page)
        {
            var result=await _monhocService.getAllMonhoc(userid,page);
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> searchMonhoc(string key)
        {
            var result =await _monhocService.searchMonhoc(key);
            return Ok(result);
        }

        [HttpGet("Loc")]//0=>theo ten ; 1=>theo lan truy cap gan nhat
        public async Task<IActionResult>locMonhoc(int option, int user_id)
        {
            return Ok(await _monhocService.locMonhoc(option,user_id));
        }

        [HttpGet("Loc_theo_tinh_trang")]
        public async Task<IActionResult>loctheoTinhTrang(int status,int giangvien_id)
        {
            return Ok(await _monhocService.locMonhoc_theo_Tinhtrang(status, giangvien_id));
        }

        [HttpGet("chitiet")]
        public async Task<IActionResult>xemChiTietMonHoc([FromQuery]int id,[FromQuery]int user_id)
        {
            var result =await  _monhocService.chitietMonhoc(id,user_id);
            return Ok(result);
        }

        [HttpPut("editMonhoc")]
        public async Task<IActionResult>editMonhoc(Edit_Monhoc_Request_DTO model)
        {
            return Ok(await _monhocService.editMonhoc(model));
        }
        [HttpPut("Gui-xetduyet-Huy-xetduyet/Test")]
        public async Task<IActionResult>setTrangthai(Guiyeucau_Huyyeucau_Monhoc_Request_DTO model)
        {
            return Ok(await _monhocService.setTrangthai(model));
        }

        [HttpPost("addMonhoc")]
        public async Task<IActionResult>addMonhoc(Monhoc_Model monhoc)
        {
            return Ok(await _monhocService.addMonhoc(monhoc));
        }

        [HttpDelete("xoaMonhoc")]
        public async Task<IActionResult>deleteMonhoc(Delete_Entity_Request_DTO model)
        {
            return Ok(await _monhocService.deleteMonhoc(model));
        }

        [HttpPost("Yeuthichmonhoc")]
        public async Task<IActionResult>Yeuthichmonhoc(Yeuthich_Request_DTO model)
        {
            return Ok(await _monhocService.YeuthichMonhoc(model));
        }

        [HttpGet("Hocvien/Mondanghoc")]
        public async Task<IActionResult>xemmondanghoc(int user_id)
        {
            return Ok(await _monhocService.xemMonhocDanghoc(user_id));
        }

        [HttpPost("ThemtongquanMonhoc")]
        public async Task<IActionResult>themTongquanmonhoc(List<Them_Tongquan_Monhoc_Request_DTO> listmodel)
        {
            return Ok(await _monhocService.themTongquanMonhoc(listmodel));
        }

        [HttpPut("XetduyetMonhoc")]
        public async Task<IActionResult>xetDuyetMonHoc(Xetduyet_Request_DTO model)
        {
            return Ok(await _monhocService.xetduyetMonhoc(model));
        }

        [HttpGet("Mondangday")]
        public async Task<IActionResult>MOndangady(int giangvien_Id)
        {
            return Ok(await _monhocService.Mondangday(giangvien_Id));
        }

        [HttpGet("Xem_Monhoc_Gansao")]
        public async Task<IActionResult>Xemmonhocgansao(int hocvien_id, int option)
        {
            return Ok(await _monhocService.Xem_List_monhoc_Yeuthich(hocvien_id, option));
        }
        
    }
}
