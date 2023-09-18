using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Monhoc")]
    public class Monhoc_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MonhocID { get; set; }
        [Required]
        [MaxLength(50)]
        public string TenMonhoc { get; set; }
        [Required]
        [MaxLength(20)]
        public string MaMonhoc { get; set; }
        [Required]
        public string Mota { get; set; }
        public int Tinhtrang { get; set; }
        public int? TobomonId { get; set; }
        public virtual Tobomon_Db Tobomon { get; set; }
        public int? TeacherId { get; set; }

        public virtual List<Lopgiangday_Db> ListLopgiangday { get; set; }
        public virtual List<Tailieu_Baigiang_Db> ListTailieu_Baigiang { get; set; }
        public virtual List<Dethi_Db> ListDethi { get; set; }
        public virtual List<QA_Db> ListCauhoi { get; set; }
        public virtual User_Db? User { get; set; }

    }
}
