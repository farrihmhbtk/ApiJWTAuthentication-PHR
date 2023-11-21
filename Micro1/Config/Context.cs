using Micro1.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Micro1.Config
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<SysConfig> SysConfig { get; set; }
        public DbSet<EquipmentMaster> EquipmentMaster { get; set; }
        public DbSet<Brand> Brand { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SysConfig>().HasData(
                new SysConfig { Id = 1, SysKey = "PHRAPPKEYOcelot", SysValue = "API123", Detail = "-" }
            );

            modelBuilder.Entity<EquipmentMaster>().HasData(
                new EquipmentMaster { Id = 1, Name = "PUMP-01", Merk = "GE", Type = "PUMP" },
                new EquipmentMaster { Id = 2, Name = "PUMP-02", Merk = "GE", Type = "PUMP" },
                new EquipmentMaster { Id = 3, Name = "PUMP-03", Merk = "GE", Type = "PUMP" },
                new EquipmentMaster { Id = 4, Name = "MOTOR-01", Merk = "PANASONIC", Type = "MOTOR" },
                new EquipmentMaster { Id = 5, Name = "MOTOR-02", Merk = "PANASONIC", Type = "MOTOR" }
            );

            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "GE", Status = "Active" },
                new Brand { Id = 2, Name = "PANASONIC", Status = "Active" }
            );
        }
    }
}
