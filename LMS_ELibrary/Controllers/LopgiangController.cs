using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_ELibrary.ServiceInterface;
using LMS_ELibrary.Model;
using Microsoft.AspNetCore.Authorization;
using LMS_ELibrary.Model.DTO;

namespace LMS_ELibrary.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    
    public class LopgiangController : ControllerBase
    {
        private readonly ILopgiangService _lopgiangservice;
        public LopgiangController(ILopgiangService lopgiangservice)
        {
            _lopgiangservice = lopgiangservice;
        }
        [HttpGet("getAll")]
        public async Task<IActionResult> getall()
        {
            return Ok(await _lopgiangservice.getAllLopgiang());
        }

        [HttpGet("Hocvien/Lopdangtheohoc")]
        public async Task<IActionResult> Lopdangtheohoc(int hocvien_id)
        {
            return Ok(await _lopgiangservice.lopDangTheoHoc(hocvien_id));
        }

        [HttpGet("detailLopgiangday")]
        public async Task<IActionResult>detailop(int id)
        {
            return Ok(await _lopgiangservice.detailLopgiangday(id));
        }

        [HttpPost("addLopgiang")]
        public async Task<IActionResult> addLopgiang(Lopgiangday_Model lopgiang)
        {
            return Ok(await _lopgiangservice.addLopgiang(lopgiang));
        }

        [HttpPost("Hocvien/Xeplopchohocvien")]
        public async Task<IActionResult>xepLopchoHocvien(Hocvien_Lop_Model model)
        {
            return Ok(await _lopgiangservice.xepLopChoHocVien(model));
        }
        [HttpPost("Themhocvienvaolop")]
        public async Task<IActionResult>themhocvienvaolop(them_Hocvien_vao_Lop_Request_DTO model)
        {
            return Ok(await _lopgiangservice.themHocvienVaolop(model));
        }

        [HttpPut("editLopgiang/{lopgiang_id}")]
        public async Task<IActionResult>editLopgiang(int lopgiang_id,Lopgiangday_Model lopgiang)
        {
            return Ok(await _lopgiangservice.editLopgiang(lopgiang_id, lopgiang));
        }

        [HttpDelete("xoaLopgiang/{lopgiang_id}")]
        public async Task<IActionResult>deleteLopgiang(int lopgiang_id)
        {
            return Ok(await _lopgiangservice.deleteLopgiang(lopgiang_id));
        }
    }
}
