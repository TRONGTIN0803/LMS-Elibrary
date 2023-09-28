using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Thongbao_Model
    {
        public int? ThongbaoID { get; set; }
        public string? Tieude { get; set; }
       
        public string? Noidung { get; set; }
        
        public DateTime? Thoigian { get; set; }
        public int? UserID { get; set; }
        


        
        
    }
}
