using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Avt_Model
    {
        public string? Path { get; set; }
        
        public double? Size { get; set; }
       
        public DateTime? Ngay_tai_len { get; set; }
        public int? UserId { get; set; }
        public virtual User_Model? User { get; set; }
    }
}
