namespace LMS_ELibrary.Model.DTO
{
    public class Edit_Monhoc_Request_DTO
    {
        public int User_Id { get; set; }
        public int Monhoc_Id { get; set; }
        public string? Monhoc_Name { get; set; }
        public string? Mota { get; set; }
        public string? MaMonhoc { get; set; }
    }
}
