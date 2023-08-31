using LMS_ELibrary.Model;
using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPut("edit/{id}")]
        public async Task<IActionResult>editChudu(int id,Chude_Model chude)
        {
            return Ok(await _chudeService.editChude(id,chude));
        }
    }
}
