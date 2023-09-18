using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("MonhocYeuthich_Db")]
    public class MonhocYeuthich_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MonhocyeuthichID { get; set; }
        public int? UserId { get; set; }
        public int? MonhocId { get; set; }

        public virtual User_Db? User { get; set; }
        public virtual Monhoc_Db? Monhoc { get; set; }
    }
}
