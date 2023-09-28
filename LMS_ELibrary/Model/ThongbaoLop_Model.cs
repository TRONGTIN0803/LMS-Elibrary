using LMS_ELibrary.Data;

namespace LMS_ELibrary.Model
{
    public class ThongbaoLop_Model
    {
        public int? ThongbaoLopID { get; set; }
        public int? Thongbao_Id { get; set; }
        public int? Lopgiang_Id { get; set; }
        public string? Status { get; set; }

        public virtual Thongbao_Model? Thongbao { get; set; }
    }
}
