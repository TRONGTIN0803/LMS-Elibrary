using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Chude_Model
    {
        public int? ChudeID { get; set; }
        public string? Tenchude { get; set; }
        public int? Monhoc_Id { get; set; }
        public virtual List<Tailieu_Baigiang_Model>? ListTailieu_Baigiang { get; set; }
    }
}
