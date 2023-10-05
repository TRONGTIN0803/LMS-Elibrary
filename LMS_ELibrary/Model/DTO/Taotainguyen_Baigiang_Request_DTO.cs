namespace LMS_ELibrary.Model.DTO
{
    public class Taotainguyen_Baigiang_Request_DTO
    {
        public int? User_Id { get; set; }
        public int? Baigiang_Id { get; set; }
        public List<int> List_Tainguyen_Id { get; set; }
    }
}
