using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class QA_Model
    {
        public string? Cauhoi { get; set; }
      
        public string? Cautrl { get; set; }
        public int? MonhocID { get; set; }
  
        public DateTime? Lancuoisua { get; set; }
    }
}
