using ApiGatewayOcelot.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics.Metrics;

namespace ApiGatewayOcelot.Config
{
    public class Context : DbContext
    {
       
        public Context(DbContextOptions<Context> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<KeyClient> KeyClient { get; set; }
        public DbSet<KeyClientRoute> KeyClientRoute { get; set; }

        public dynamic[] GetKeyClientData()
        {
            var keyClientData = KeyClient.ToList();
            var dynamicArray = keyClientData.Select(item => (dynamic)item).ToArray();

            return dynamicArray;
        }
        public dynamic[] GetKeyClientRouteData()
        {
            var keyClientRouteData = KeyClientRoute.ToList();
            var dynamicArray = keyClientRouteData.Select(item => (dynamic)item).ToArray();

            return dynamicArray;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            int IdKeyClient = 1;
            modelBuilder.Entity<KeyClient>().HasData(
                new KeyClient { Id = IdKeyClient++, KeyClientName = "KeyClientName_user1", Status = "Active" },
                new KeyClient { Id = IdKeyClient++, KeyClientName = "KeyClientName_user2", Status = "Active" },
                new KeyClient { Id = IdKeyClient++, KeyClientName = "KeyClientName_user3", Status = "Active" },
                new KeyClient { Id = IdKeyClient++, KeyClientName = "KeyClientName_user4", Status = "Active" }
            );
            int IdKeyClientRoute = 1;
            modelBuilder.Entity<KeyClientRoute>().HasData(
                // KeyClientName_user1
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "POST" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "DELETE" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/Brand", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/Brand", Method = "POST" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/Brand", Method = "DELETE" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellMaster", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellMaster", Method = "POST" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellMaster", Method = "DELETE" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellTest", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellTest", Method = "POST" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellTest", Method = "DELETE" },
                // KeyClientName_user2
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "POST" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Equipment/Brand", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Equipment/Brand", Method = "POST" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Well/WellMaster", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Well/WellTest", Method = "GET" },
                // KeyClientName_user3
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Equipment/Brand", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Well/WellMaster", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Well/WellMaster", Method = "POST" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Well/WellTest", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Well/WellTest", Method = "POST" },
                // KeyClientName_user4
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user4", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "GET" },
                new KeyClientRoute { Id = IdKeyClientRoute++, KeyClientName = "KeyClientName_user4", Route = "/ApiGateway/Equipment/Brand", Method = "GET" }
            );
        }
    }
}
