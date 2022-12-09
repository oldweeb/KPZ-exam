using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace back_end.Models
{
    [Table("Animals")]
    [Index(nameof(Name), IsUnique = false)]
    public class Animal
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public Guid Id { get; set; }
        [Column("Name", TypeName = "nvarchar(255)")]
        public string? Name { get; set; }
        [Column("DateOfBirth", TypeName = "date")]
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Column("Owner", TypeName = "nvarchar(255)")]
        [Required]
        public string OwnerName { get; set; }
        [ForeignKey("AnimalTypeFK")]
        public AnimalType AnimalType { get; set; }
        [System.Text.Json.Serialization.JsonIgnore]
        public ICollection<Visit> Visits { get; set; }
    }
}
