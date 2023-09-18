using LMS_ELibrary.Model;
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
        [HttpGet("HoidapBaigiang")]
        public async Task<IActionResult> CauhoiBaigiang([FromQuery]int baigiangId)
        {
            return Ok(await _hoidapServicce.XemhoidapBaigiang(baigiangId));
        }

        [HttpPost("DatcauhoichoBaigiang")]
        public async Task<IActionResult>datcauhoi(CauhoiVandap_Model model)
        {
            return Ok(await _hoidapServicce.DatcauhoiBaigang(model));
        }

        [HttpPost("TrlCauhoi")]
        public async Task<IActionResult>Trlcauhoi(Cautrl_Model model)
        {
            return Ok(await _hoidapServicce.TrlCauhoi(model));
        }
    }
}
