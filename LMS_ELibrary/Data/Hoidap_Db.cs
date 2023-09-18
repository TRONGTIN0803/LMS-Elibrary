using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Hoidap")]
    public class Hoidap_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CauhoiID { get; set; }
        public string? Tieude { get; set; }
        public string? Cauhoi { get; set; }
        public DateTime? Ngaytao { get; set; }
        public int? UserId { get; set; }
        public int? BaigiangId { get; set; }
        public int? LopgiangId { get; set; }
        public int? ChudeId { get; set; }

        public virtual User_Db? Nguoihoi { get; set; }
        public virtual Tailieu_Baigiang_Db? Baigiang { get; set; }
        public virtual Lopgiangday_Db? Lopgiang { get; set; }
        public virtual Chude_Db? Chude { get; set; }

        public virtual List<Cautrl_Db>? listCautrl { get; set; }

    }
}
