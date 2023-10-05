using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("File_Tailen")]
    public class File_Tailen_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int File_Tailen_Id { get; set; }
        public string? Tenfile { get; set; }
        public double? Size { get; set; }
        public string? Path { get; set; }
        public int? Nguoitailen_Id { get; set; }
        public DateTime? Ngaytailen { get; set; }
        public int? Status { get; set; }
        public int? Type { get; set; }
        public int? Nguoiduyet_Id { get; set; }
        public DateTime? Ngayduyet { get; set; }
        public string? Ghichu { get; set; }

        public virtual User_Db? Nguoitailen { get; set; }
        public virtual List<Tailieu_Baigiang_Db>? list_Baigiang { get; set; }
        public virtual List<Tainguyen_Db>? list_Tainguyen { get; set; }
    }
}
