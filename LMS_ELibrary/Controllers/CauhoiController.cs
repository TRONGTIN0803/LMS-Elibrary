using LMS_ELibrary.Model;
using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CauhoiController : ControllerBase
    {
        private readonly CauhoiService _cauhoiSerivce;
        public CauhoiController(CauhoiService cauhoiSerivce)
        {
            _cauhoiSerivce = cauhoiSerivce;
        }

        [HttpGet("getallCauhoi")]
        public async Task<IActionResult> getall()
        {
            return Ok(await _cauhoiSerivce.getAllCauhoi());
        }

        [HttpGet("xemCauHoitheoMon/{id_monhoc}")]
        public async Task<IActionResult>CauhoitheoMon(int id_monhoc)
        {
            return Ok(await _cauhoiSerivce.xemCauhoitheoMon(id_monhoc));
        }

        [HttpGet("xemCauHoitheoToMon/{id_tomon}")]
        public async Task<IActionResult> CauHoitheoToMon(int id_tomon)
        {
            return Ok(await _cauhoiSerivce.xemCauHoitheoToMon(id_tomon));
        }

        [HttpGet("chitietCauhoi/{idcauhoi}")]
        public async Task<IActionResult>chitietcauhoi(int idcauhoi)
        {
            return Ok(await _cauhoiSerivce.chitietCauhoi(idcauhoi));
        }

        [HttpPost("addCauhoi")]
        public async Task<IActionResult>addCauhoi(QA_Model cauhoi)
        {
            return Ok(await _cauhoiSerivce.addCauhoi(cauhoi));
        }

        [HttpPut("editCauhoi/{idcauhoi}")]
        public async Task<IActionResult>editCauhoi(int idcauhoi,QA_Model cauhoi)
        {
            return Ok(await _cauhoiSerivce.editCauhoi(idcauhoi, cauhoi));
        }

        [HttpDelete("xoaCauhoi/{idcauhoi}")]
        public async Task<IActionResult> xoacauhoi(int idcauhoi)
        {
            return Ok(await _cauhoiSerivce.xoaCauhoi(idcauhoi));
        }
    }
}
