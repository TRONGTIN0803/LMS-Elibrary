using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Hocvien_Lop")]
    public class Hocvien_Lop_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int HvLopID { get; set; }
        public int? User_Id { get; set; }
        public int? Lopgiang_Id { get; set; }

        public virtual User_Db? User { get; set; }
        public virtual Lopgiangday_Db? Lopgiang { get; set; }
    }
}
