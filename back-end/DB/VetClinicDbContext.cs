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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Animal>().Navigation(a => a.AnimalType).AutoInclude();
            modelBuilder.Entity<Animal>().Navigation(a => a.Visits).AutoInclude();
            modelBuilder.Entity<Visit>().Navigation(v => v.Animal).AutoInclude();

            base.OnModelCreating(modelBuilder);
        }
    }
}
