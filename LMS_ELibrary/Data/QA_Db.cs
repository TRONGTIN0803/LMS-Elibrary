using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("QA")]
    public class QA_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int QAID { get; set; }
        [Required]
        [MaxLength(300)]
        public string Cauhoi { get; set; }
        [Required]
        public string Cautrl { get; set; }
        public int? MonhocID { get; set; }
        public virtual Monhoc_Db? Monhoc { get; set; }
        public DateTime? Lancuoisua { get; set; }
        public int? Dokho { get; set; }
        public string? Macauhoi { get; set; }
        public int? Nguoitao_Id { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual List<Ex_QA_Db>? ListExQA { get; set; }
        public virtual User_Db? Nguoitao { get; set; }
    }
}
