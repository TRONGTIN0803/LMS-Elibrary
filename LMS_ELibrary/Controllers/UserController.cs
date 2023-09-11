﻿using LMS_ELibrary.Model;
using LMS_ELibrary.Services;
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

        [HttpPut("UpdateAvt/{user_id}")]
        public async Task<IActionResult>upAVT(int user_id, IFormFile file)
        {
            return Ok(await _userService.UpLoadAvt(user_id,file));
        }

        [HttpPut("changePass/{user_id}")]
        public async Task<IActionResult>changePass(int user_id,ChangePass pass)
        {
            return Ok(await _userService.changePassword(user_id, pass));
        }
    }
}
