using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS_ELibrary.Data
{
    [Table("Tobomon")]
    public class Tobomon_Db
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TobomonId { get; set; }
        [Required]
        public string TobomonName { get; set; }

        public virtual List<Monhoc_Db> ListMonhoc { get; set; }
    }
}
