using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Ex_QA")]
    public class Ex_QA_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EXQAID { get; set; }
        public int? DethiID { get; set; }
        public virtual Dethi_Db Dethi { get; set; }
        public int? QAID { get; set; }
        public virtual QA_Db QA { get; set; }
    }
}
