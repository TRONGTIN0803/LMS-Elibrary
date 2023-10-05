using LMS_ELibrary.Data;

namespace LMS_ELibrary.Model
{
    public class File_Tailen_Model
    {
        public int? File_Tailen_Id { get; set; }
        public string? Tenfile { get; set; }
        public double? Size { get; set; }
        public string? Path { get; set; }
        public int? Nguoitailen_Id { get; set; }
        public DateTime? Ngaytailen { get; set; }
        public string? Status { get; set; }
        public string? Type { get; set; }
        public int? Nguoiduyet_Id { get; set; }
        public DateTime? Ngayduyet { get; set; }
        public string? Ghichu { get; set; }

        public virtual User_Model? Nguoitailen { get; set; }
        public virtual List<Tailieu_Baigiang_Model>? list_Baigiang { get; set; }
        public virtual List<Tainguyen_Model>? list_Tainguyen { get; set; }
    }
}
