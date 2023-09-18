﻿using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LMS_ELibrary.Data
{
    [Table("Dethi")]
    public class Dethi_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int DethiID { get; set; }
        [Required]
        [MaxLength(20)]
        public string Madethi { get; set; }
        [Required]
        public int Status { get; set; }
        public int? UserID { get; set; }
        
        [Required]
        public DateTime Ngaytao { get; set; }
        public int? MonhocID { get; set; }
        
        public int? FileId { get; set; }
        public int? NguoiduyetId { get; set; }
        public DateTime? Ngayduyet { get; set; }

        public virtual File_Dethi_Db? File_Dethi { get; set; }
        public virtual User_Db? User { get; set; }
        public virtual Monhoc_Db? Monhoc { get; set; }
        public virtual List<Ex_QA_Db>? ListExQA { get; set; }
    }
}
