using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Thongbao_Model
    {
        public string? Tieude { get; set; }
       
        public string? Noidung { get; set; }
        
        public DateTime? Thoigian { get; set; }
        public int? UserID { get; set; }
        
        public int? Status { get; set; }

        public List<int>? list_Nguoinhan { get; set; }
        public List<int>? list_Lopgiang { get; set; }
    }
}
