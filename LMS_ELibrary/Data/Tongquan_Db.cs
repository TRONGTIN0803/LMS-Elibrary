using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Tongquan")]
    public class Tongquan_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TongquanID { get; set; }
        public int? Monhoc_Id { get; set; }
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }

        public virtual Monhoc_Db? Monhoc { get; set; }
    }
}
