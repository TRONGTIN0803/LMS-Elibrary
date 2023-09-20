using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Role")]
    public class Role_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RoleId { get; set; }
        public string? Tenvaitro { get; set; }
        public string? Mota { get; set; }
        public int? Phanquyen { get; set; }

        public virtual List<User_Db>? listUser { get; set; }
    }
}
