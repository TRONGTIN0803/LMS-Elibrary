using LMS_ELibrary.Data;

namespace LMS_ELibrary.Model
{
    public class Role_Model
    {
        public int? RoleId { get; set; }
        public string? Tenvaitro { get; set; }
        public string? Mota { get; set; }
        public int? Phanquyen { get; set; }

        public virtual List<User_Model>? listUser { get; set; }
    }
}
