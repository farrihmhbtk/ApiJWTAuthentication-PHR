using Micro2.Models;
using Microsoft.EntityFrameworkCore;

namespace Micro2.Config
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<WellMaster> WellMaster { get; set; }
        public DbSet<WellTest> WellTest { get; set; }
        public DbSet<SysConfig> SysConfig { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SysConfig>().HasData(
                new SysConfig { Id = 1, SysKey = "PHRAPPKEYOcelot", SysValue = "API123", Detail = "-" }
            );

            modelBuilder.Entity<WellMaster>().HasData(
                new WellMaster { Id = 1, Name = "WELL-01", Area = "DURI", Type = "OP" },
                new WellMaster { Id = 2, Name = "WELL-02", Area = "DURI", Type = "OP" },
                new WellMaster { Id = 3, Name = "WELL-03", Area = "MINAS", Type = "WI" }
            );

            modelBuilder.Entity<WellTest>().HasData(
                new WellTest { Id = 1, Name = "WELL-01", Date = new DateTime(2023, 1, 1), Bopd = 100 },
                new WellTest { Id = 2, Name = "WELL-01", Date = new DateTime(2023, 2, 1), Bopd = 80 }
            );
        }
    }
}
