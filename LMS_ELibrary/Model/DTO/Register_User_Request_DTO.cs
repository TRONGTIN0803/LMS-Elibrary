namespace LMS_ELibrary.Model.DTO
{
    public class Register_User_Request_DTO
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
       
        public string? UserFullname { get; set; }
        public bool? Gioitinh { get; set; }
        public string? Email { get; set; }
        public string? Sdt { get; set; }
        public string? Diachi { get; set; }
        public string? Nganh { get; set; }
    }
}
