namespace LMS_ELibrary.Model.DTO
{
    public class Danhdauthongbao_Request_DTO
    {
        public int User_Id { get; set; }
        public int Status { get; set; }
        public List<int> List_Id_Thongbao { get; set; }
    }
}
