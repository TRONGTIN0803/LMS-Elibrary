using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Data
{
    [Table("FileDethi")]
    public class File_Dethi_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? FileId { get; set; }
        public int? User_Id { get; set; }
        public string? Path { get; set; }
        public double? Size { get; set; }
        public DateTime? NgayTao { get; set; }

        public virtual User_Db? User { get; set; }
        public virtual List<Dethi_Db>? listDethi { get; set; }

    }
}
