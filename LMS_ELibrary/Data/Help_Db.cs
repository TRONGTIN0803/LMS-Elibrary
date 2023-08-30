using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Help")]
    public class Help_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HelpID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Tieude { get; set; }
        [Required]
        public string Noidung { get; set; }
        [Required]
        public DateTime NgayGui { get; set; }
        public int? UserID { get; set; }
        public virtual User_Db User { get; set; }
    }
}
