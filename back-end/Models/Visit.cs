using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace back_end.Models
{
    [Table("Visits")]
    public class Visit
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        [Required]
        [ForeignKey("AnimalFK")]
        public Animal Animal { get; set; }
        [Required]
        [Column("DateOfVisit", TypeName = "datetime")]
        public DateTime DateOfVisit { get; set; }
        [Column("Diagnosis", TypeName = "nvarchar(255)")]
        [Required]
        public string Diagnosis { get; set; }
    }
}
