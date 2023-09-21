using LMS_ELibrary.Data;

namespace LMS_ELibrary.Model
{
    public class CauhoiYeuthich_Model
    {
        public int? CauhoiYeuthichID { get; set; }
        public int? UserId { get; set; }
        public int? CauhoiId { get; set; }

        public virtual User_Model? User { get; set; }
        public virtual CauhoiVandap_Model? Cauhoi { get; set; }
    }
}
