using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Cautrl")]
    public class Cautrl_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CautrlID { get; set; }
        public string? Cautrl { get; set; }
        public DateTime? Ngaytao { get; set; }
        public int? UserId { get; set; }
        public int? CauhoiId { get; set; }

        public virtual User_Db? User { get; set; }
        public virtual CauhoiVandap_Db? Cauhoi { get; set; }
    }
}
