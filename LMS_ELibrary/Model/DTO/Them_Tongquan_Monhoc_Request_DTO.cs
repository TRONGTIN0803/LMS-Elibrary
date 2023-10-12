namespace LMS_ELibrary.Model.DTO
{
    public class Them_Tongquan_Monhoc_Request_DTO
    {
        public int? Monhoc_Id { get; set; }
        public int? Giangvien_Id { get; set; }
        public List<Tongquan_Monhoc>? list_Tong_Quan_Mon_Hoc { get; set; }
    }
}
