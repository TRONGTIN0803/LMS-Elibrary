using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Avt")]
    public class Avt_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AvtID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Path { get; set; }
        [Required]
        public double Size { get; set; }
        [Required]
        public DateTime Ngay_tai_len { get; set; }
        public int? UserId { get; set; }
        public virtual User_Db? User { get; set; }


    }
}
