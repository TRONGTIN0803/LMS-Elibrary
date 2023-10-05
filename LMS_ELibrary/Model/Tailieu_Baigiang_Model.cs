using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Tailieu_Baigiang_Model
    {

        public int? DocId { get; set; }
        public string? TenDoc { get; set; }
        public int? MonhocID { get; set; }      
        public int? UserID { get; set; }      
        public int? ChudeID { get; set; }      
        public DateTime? Sualancuoi { get; set; }
        public double? Kichthuoc { get; set; }
        public string? Path { get; set; }
       
        public string? Status { get; set; }
       
        public string? Type { get; set; }
        public int? Nguoiduyet { get; set; }
        public DateTime? NgayDuyet { get; set; }
        public string? Ghichu { get; set; }

        public virtual User_Model? User { get; set; }
        public virtual Monhoc_Model? Monhoc { get; set; }
        public virtual Chude_Model? Chude { get; set; }
        public virtual File_Tailen_Model? File_Baigiang { get; set; }
        public virtual List<Tainguyen_Model>? List_File_Tainguyen { get; set; }
    }
}
