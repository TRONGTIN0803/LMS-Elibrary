using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Dethi_Model
    { 
        public string? Madethi { get; set; }
        
        public int? Status { get; set; }
        public int? UserID { get; set; }
        
       
        public DateTime? Ngaytao { get; set; }
        public int? MonhocID { get; set; }
        
        public string? File_Path { get; set; }
    }
}
