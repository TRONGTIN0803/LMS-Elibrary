using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("LopGiangday")]
    public class Lopgiangday_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LopgiangdayID { get; set; }
        public int? UserID { get; set; }
        public virtual User_Db User { get; set; }
        public int? MonhocID { get; set; }
        public virtual Monhoc_Db Monhoc { get; set; }
        [Required]
        public string TenLop { get; set; }
        [Required]
        public DateTime Thoigian { get; set; }
        public DateTime? Truycapgannhat { get; set; }

    }
}
