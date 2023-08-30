using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class User_Model
    {
        public User_Model(string? userName, string? password)
        {
            UserName = userName;
            Password = password;
        }

        public User_Model()
        {
        }

        public string? UserName { get; set; }
     
        public string? Password { get; set; }

        public string? Role { get; set; }
        public string? Avt { get; set; }
     
        public string? UserFullname { get; set; }
        public string? Gioitinh { get; set; }
       
        public string? Email { get; set; }
      
        public string? Sdt { get; set; }
        public string? Diachi { get; set; }
    }
}
