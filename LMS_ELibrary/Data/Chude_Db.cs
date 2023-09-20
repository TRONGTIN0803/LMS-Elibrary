using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Chude")]
    public class Chude_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ChudeID { get; set; }
        [Required]
        [MaxLength(100)]
        public string Tenchude { get; set; }
        public int? Monhoc_Id { get; set; }
        

        public virtual List<Tailieu_Baigiang_Db>? ListTailieu_Baigiang { get; set; }
        public virtual Monhoc_Db? Monhoc { get; set; }
    }
}
