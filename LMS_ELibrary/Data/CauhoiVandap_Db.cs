using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("CauhoiVandap")]
    public class CauhoiVandap_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CauhoiId { get; set; }
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }
        public DateTime? Ngaytao { get; set; }
        public int? UserId { get; set; }
        public int? TailieuId { get; set; }
        public int? LopgiangId { get; set; }
        public int? ChudeId { get; set; }

        public virtual User_Db? User { get; set; }
        public virtual Tailieu_Baigiang_Db? Tailieu { get; set; }
        public virtual List<Cautrl_Db>? list_Cautrl { get; set; }
        public virtual List<CauhoiYeuthich_Db>? list_Cauhoiyeuthich { get; set; }
    }
}
