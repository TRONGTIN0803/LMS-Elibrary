using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Help_Model
    {
        public string? Tieude { get; set; }
       
        public string? Noidung { get; set; }
       
        public DateTime NgayGui { get; set; }
        public int? UserID { get; set; }
    }
}
