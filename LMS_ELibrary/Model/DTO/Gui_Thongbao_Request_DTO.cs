namespace LMS_ELibrary.Model.DTO
{
    public class Gui_Thongbao_Request_DTO
    {
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }
        public int? ID_Nguoigui { get; set; }

        public List<int>? list_ID_Nguoinhan { get; set; }
        public List<int>? list_ID_Lopgiang { get; set; }
    }
}
