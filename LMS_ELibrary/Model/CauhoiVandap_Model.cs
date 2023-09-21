using LMS_ELibrary.Data;

namespace LMS_ELibrary.Model
{
    public class CauhoiVandap_Model
    {
        public int CauhoiId { get; set; }
        public string? Tieude { get; set; }
        public string? Noidung { get; set; }
        public DateTime? Ngaytao { get; set; }
        public int? UserId { get; set; }
        public int? TailieuId { get; set; }
        public int? LopgiangId { get; set; }
        public int? ChudeId { get; set; }
        public int? TongsoCautrl { get; set; }

        public virtual User_Model? User { get; set; }
        public virtual Tailieu_Baigiang_Model? Tailieu { get; set; }
        public virtual List<Cautrl_Db>? list_Cautrl { get; set; }
        public virtual List<CauhoiYeuthich_Db>? list_Cauhoiyeuthich { get; set; }
    }
}
