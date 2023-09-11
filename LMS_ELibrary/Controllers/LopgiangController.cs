using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_ELibrary.ServiceInterface;

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

        [HttpGet("detailLopgiangday/{id}")]
        public async Task<IActionResult>detailop(int id)
        {
            return Ok(await _lopgiangservice.detailLopgiangday(id));
        }
    }
}
