using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Monhoc_Model
    {
        public string? TenMonhoc { get; set; }
        
        public string? MaMonhoc { get; set; }
 
        public string? Mota { get; set; }
        public int? Tinhtrang { get; set; }
        public int? TobomonId { get; set; }

        public virtual Tobomon_Model? Tobomon { get; set; }

        public virtual List<Tailieu_Baigiang_Model> ListTailieu_Baigiang { get; set; }
        public virtual List<Lopgiangday_Model> ListLopgiangday { get; set; }

    }
}
