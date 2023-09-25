using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Lopgiangday_Model
    {
        public int? LopgiangdayID { get; set; }
        public int? UserID { get; set; }
        public string? Malop { get; set; }
        public int? MonhocID { get; set; }
        public string? TenLop { get; set; }
        public DateTime Thoigian { get; set; }
        public DateTime? Truycapgannhat { get; set; }

        public virtual User_Model? User { get; set; }
        public virtual Monhoc_Model? Monhoc { get; set; }
    }
}
