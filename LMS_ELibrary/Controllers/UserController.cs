using LMS_ELibrary.Model;
using LMS_ELibrary.Model.DTO;
using LMS_ELibrary.ServiceInterface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS_ELibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        
        public async Task<IActionResult> Login(User_Model user)
        {
            var result = await _userService.Login(user);
            return Ok(result);
        }

        [HttpGet("checkInfor/{user_id}")]
        public async Task<IActionResult> checkInfor(int user_id)
        {
            return Ok(await _userService.checkInfor(user_id)); 
        }

        [HttpPost("Register_Account_Hocvien")]
        public async Task<IActionResult>registerHV(Register_User_Request_DTO model)
        {
            return Ok(await _userService.add_Account_Hocvien(model));
        }

        [HttpPost("Register_Account_Giangvien")]
        public async Task<IActionResult> registerGV(Register_User_Request_DTO model)
        {
            return Ok(await _userService.add_Account_Giangvien(model));
        }

        [HttpPut("UpdateAvt/{user_id}")]
        public async Task<IActionResult>upAVT(int user_id, IFormFile file)
        {
            return Ok(await _userService.UpLoadAvt(user_id,file));
        }

        [HttpPut("changePasshahaha/{user_id}")]
        public async Task<IActionResult>changePass(int user_id,ChangePass pass)
        {
            return Ok(await _userService.changePassword(user_id, pass));
        }

        [HttpGet("Avt_da_tai_len/{user_id}")]
        public async Task<IActionResult>Avtdatailen(int user_id)
        {
            return Ok(await _userService.Avt_da_tai_len(user_id));
        }

        [HttpDelete("xoaAccount")]
        public async Task<IActionResult>xoaAccount(User_Model model)
        {
            return Ok(await _userService.xoaAccount(model));
        }

        [HttpPost("ThemRole")]
        public async Task<IActionResult>themRole(Role_Model model)
        {
            return Ok(await _userService.ThemRole(model));
        }
    }
}
