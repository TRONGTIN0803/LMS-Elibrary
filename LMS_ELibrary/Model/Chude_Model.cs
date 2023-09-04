using LMS_ELibrary.Data;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Model
{
    public class Chude_Model
    {
        public string? Tenchude { get; set; }
        public virtual List<Tailieu_Baigiang_Model>? ListTailieu_Baigiang { get; set; }
    }
}
