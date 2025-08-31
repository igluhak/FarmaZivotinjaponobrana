using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace FarmaZivotinja.Data
{
    public class FarmDbContext : DbContext
    {
        public DbSet<AnimalEntity> Animals => Set<AnimalEntity>();
        public DbSet<ProductEntity> Products => Set<ProductEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=farm.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnimalEntity>().ToTable("Animals");
        }
    }

    public class AnimalEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Species { get; set; } = "";
        public int Age { get; set; }
        public double WeightKg { get; set; }
    }

    public class ProductEntity
    {
        [Key]
        public int Id { get; set; }
        public string Type { get; set; } = "";
        public int Quantity { get; set; }
    }
}
