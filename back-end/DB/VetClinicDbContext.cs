using back_end.Models;
using Microsoft.EntityFrameworkCore;

namespace back_end.DB
{
    public class VetClinicDbContext : DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<AnimalType> AnimalTypes { get; set; }
        public DbSet<Visit> Visits { get; set; }

        public VetClinicDbContext(DbContextOptions<VetClinicDbContext> options) : base(options)
        {
        }
    }
}
