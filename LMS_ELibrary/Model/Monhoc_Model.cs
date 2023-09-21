using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Monhoc_Model
    {
        public int? MonhocID { get; set; }
        public string? TenMonhoc { get; set; }
        
        public string? MaMonhoc { get; set; }
 
        public string? Mota { get; set; }
        public string? Tinhtrang { get; set; }
        public int? TobomonId { get; set; }
        public int? UserId { get; set; }
        public string? Giangvien { get; set; }
        public string? TrangthaiYeuthich { get; set; }
        public string? TailieuPheduyet { get; set; }

        public virtual Tobomon_Model? Tobomon { get; set; }

        public virtual List<Tailieu_Baigiang_Model>? ListTailieu_Baigiang { get; set; }
        public virtual List<Lopgiangday_Model>? ListLopgiangday { get; set; }

    }
}
