namespace LMS_ELibrary.Model.DTO
{
    public class Tao_dethi_tu_nganhangcauhoi_Request_DTO
    {
        public string? Madethi { get; set; }
        public int? UserID { get; set; }
        public int? MonhocID { get; set; }
        public List<int>? List_Cauhoi_Id { get; set; }
    }
}
