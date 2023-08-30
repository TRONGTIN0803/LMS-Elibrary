using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Numerics;

namespace LMS_ELibrary.Data
{
    [Table("Tailieu_Baigiang")]
    public class Tailieu_Baigiang_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DocId { get; set; }
        public int? UserId { get; set; }
        public virtual User_Db User { get; set; }
        [Required]
        public string TenDoc { get; set; }
        public int? MonhocID { get; set; }
        public virtual Monhoc_Db Monhoc { get; set; }
        public int? ChudeID { get; set; }
        public virtual Chude_Db Chude { get; set; }
        public DateTime? Sualancuoi { get; set; }
        public BigInteger? Kichthuoc { get; set; }
        public string? Path { get; set; }
        [Required]
        public int Status { get; set; }
        [Required]
        public int Type { get; set; }
    }
}
