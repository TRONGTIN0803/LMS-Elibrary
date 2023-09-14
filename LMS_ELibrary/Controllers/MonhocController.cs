﻿using LMS_ELibrary.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LMS_ELibrary.ServiceInterface;
using LMS_ELibrary.Model;

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
        public async Task<IActionResult> getAllMonhoc()
        {
            var result=await _monhocService.getAllMonhoc();
            return Ok(result);
        }

        [HttpGet("search")]
        public async Task<IActionResult> searchMonhoc(string key)
        {
            var result =await _monhocService.searchMonhoc(key);
            return Ok(result);
        }

        [HttpGet("Loc")]//0=>theo ten ; 1=>theo lan truy cap gan nhat
        public async Task<IActionResult>locMonhoc(int option)
        {
            return Ok(await _monhocService.locMonhoc(option));
        }

        [HttpGet("Loc_theo_tinh_trang")]
        public async Task<IActionResult>loctheoTinhTrang(int status)
        {
            return Ok(await _monhocService.locMonhoc_theo_Tinhtrang(status));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult>xemChiTietMonHoc(int id)
        {
            var result =await  _monhocService.chitietMonhoc(id);
            return Ok(result);
        }

        [HttpPut("editMonhoc/{monhoc_id}")]
        public async Task<IActionResult>editMonhoc(int monhoc_id, Monhoc_Model monhoc)
        {
            return Ok(await _monhocService.editMonhoc(monhoc_id, monhoc));
        }
        [HttpPut("setTrangThai")]
        public async Task<IActionResult>setTrangthai([FromQuery]List<int> monhoc_id,int status)
        {
            return Ok(await _monhocService.setTrangthai(monhoc_id, status));
        }

        [HttpPost("addMonhoc")]
        public async Task<IActionResult>addMonhoc(Monhoc_Model monhoc)
        {
            return Ok(await _monhocService.addMonhoc(monhoc));
        }

        [HttpDelete("xoaMonhoc/{monhoc_id}")]
        public async Task<IActionResult>deleteMonhoc(int monhoc_id)
        {
            return Ok(await _monhocService.deleteMonhoc(monhoc_id));
        }
    }
}
