using Microsoft.EntityFrameworkCore;
using SmartERP.Development.Domain.Entities;
using System.Reflection;

namespace SmartERP.Development.Database
{
    public class DevelopmentContext : DbContext
    {
        public DbSet<CustomModule> Modules { get; set; }

        public DbSet<CustomEntity> Entities { get; set; }

        public DbSet<CustomEntityField> Fields { get; set; }

        public DbSet<CustomView> Views { get; set; }

        public DevelopmentContext()
        {
            
        }

        public DevelopmentContext(DbContextOptions<DevelopmentContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=AERO16;Initial Catalog=SmartERP;Integrated Security=True;TrustServerCertificate=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
