using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Tailieu_Baigiang_Model
    {
        public int? UserId { get; set; }
       
        public string? TenDoc { get; set; }
        public int? MonhocID { get; set; }
       
        public int? ChudeID { get; set; }
        
        public DateTime? Sualancuoi { get; set; }
        public double? Kichthuoc { get; set; }
        public string? Path { get; set; }
       
        public string? Status { get; set; }
       
        public int? Type { get; set; }
    }
}
