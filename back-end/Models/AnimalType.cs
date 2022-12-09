using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace back_end.Models
{
    [Table("AnimalTypes")]
    [Index(nameof(Name), IsUnique = true)]
    public class AnimalType
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
        [Required]
        [Column("Name", TypeName = "nvarchar(255)")]
        public string Name { get; set; }
    }
}
