﻿namespace LMS_ELibrary.Model
{
    public class Tao_cauhoi_tructiep_Request_DTO
    {
        public string? Madethi { get; set; }
        public int? Status { get; set; }
        public int? UserId { get; set; }
        public DateTime? Ngaytao { get; set; }
        public int? MonhocId { get; set; }
        public List<QA_Model>? listcauhoi { get; set; }
    }
}
