using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Thongbao")]
    public class Thongbao_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThongbaoID { get; set; }
        [Required]
        public string Tieude { get; set; }
        [Required]
        public string Noidung { get; set; }
        [Required]
        public DateTime Thoigian { get; set; }
        public int? UserID { get; set; }
        [Required]
        public int Status { get; set; }
        

        public virtual User_Db? User { get; set; }
        public virtual List<ThongbaoLop_Db>? List_ThongbaoLop { get; set; }
    }
}
