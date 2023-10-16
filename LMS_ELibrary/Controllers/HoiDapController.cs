using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HoiDapController : ControllerBase
    {
        private readonly IHoidapService _hoidapServicce;

        public HoiDapController(IHoidapService hoidapServicce)
        {
            _hoidapServicce = hoidapServicce;
        }
        [HttpGet("List_Cauhoi_Baigiang")]
        public async Task<IActionResult> List_Cauhoi_Baigiang(int user_id, int baigiangId, int lop_Id, int typeCauhoi, int filt)
        {
            return Ok(await _hoidapServicce.XemhoidapBaigiang(user_id, baigiangId,lop_Id, typeCauhoi, filt));
        }

        [HttpGet("listCauhoiYeuthich")]
        public async Task<IActionResult> cauhoityeuthich(int user_id)
        {
            return Ok(await _hoidapServicce.XemcauhoiYeuthich(user_id));
        }

        [HttpPost("DatcauhoichoBaigiang")]
        public async Task<IActionResult>datcauhoi(Datcauhoitronglop_Request_DTO model)
        {
            return Ok(await _hoidapServicce.DatcauhoiBaigang(model));
        }

        [HttpPost("TrlCauhoi")]
        public async Task<IActionResult>Trlcauhoi(Cautrl_Model model)
        {
            return Ok(await _hoidapServicce.TrlCauhoi(model));
        }

        [HttpPut("ChinhsuaCautrl")]
        public async Task<IActionResult> SuaCautrl(Cautrl_Model model)
        {
            return Ok(await _hoidapServicce.ChinhsuaCautrl(model));
        }

        [HttpPut("Yeuthichcauhoi")]
        public async Task<IActionResult> Yeuthichcauhoi(Yeuthich_Request_DTO model)
        {
            return Ok(await _hoidapServicce.YeuthichCauhoi(model));
        }

        [HttpDelete("xoaCautrl")]
        public async Task<IActionResult> xoaCautrl(Delete_Entity_Request_DTO model)
        {
            return Ok(await _hoidapServicce.Xoacautrl(model));
        }
    }
}
