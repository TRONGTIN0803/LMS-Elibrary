using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Data
{
    [Table("Dethi")]
    public class Dethi_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DethiID { get; set; }
        [Required]
        [MaxLength(20)]
        public string Madethi { get; set; }
        [Required]
        public int Status { get; set; }
        public int? UserID { get; set; }
        
        [Required]
        public DateTime Ngaytao { get; set; }
        public int? MonhocID { get; set; }
        
        public string? File_Path { get; set; }

        public virtual User_Db? User { get; set; }
        public virtual Monhoc_Db? Monhoc { get; set; }
        public virtual List<Ex_QA_Db>? ListExQA { get; set; }
    }
}
