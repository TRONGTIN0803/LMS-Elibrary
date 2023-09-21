using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("ThongbaoLop")]
    public class ThongbaoLop_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ThongbaoLopID { get; set; }
        public int? Thongbao_Id { get; set; }
        public int? Lopgiang_Id { get; set; }

        public virtual Thongbao_Db? Thongbao { get; set; }
        public virtual Lopgiangday_Db? Lopgiang { get; set; }
    }
}
