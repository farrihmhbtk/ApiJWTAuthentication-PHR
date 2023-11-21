using Ocelot.Configuration;

namespace ApiGatewayOcelot.Models
{
    public class UserManagement
    {
        public static dynamic[] KeyClientData()
        {
            int idCounter = 1;
            return new dynamic[]
            {
                new { Id = idCounter++, Status = "Active", KeyClientName = "KeyClientName_user1" },
                new { Id = idCounter++, Status = "Active", KeyClientName = "KeyClientName_user2" },
                new { Id = idCounter++, Status = "Active", KeyClientName = "KeyClientName_user3" },
                new { Id = idCounter++, Status = "Active", KeyClientName = "KeyClientName_user4" }
            };
        }
        public static dynamic[] keyClientRouteData()
        {
            int idCounter = 1;
            return new dynamic[]
            {
                // KeyClientName_user1
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "POST" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "DELETE" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/Brand", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/Brand", Method = "POST" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Equipment/Brand", Method = "DELETE" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellMaster", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellMaster", Method = "POST" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellMaster", Method = "DELETE" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellTest", Method = "GET"  },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellTest", Method = "POST"  },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user1", Route = "/ApiGateway/Well/WellTest", Method = "DELETE"  },
                // KeyClientName_user2
                new { Id = idCounter++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "POST" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Equipment/Brand", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Equipment/Brand", Method = "POST" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Well/WellMaster", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user2", Route = "/ApiGateway/Well/WellTest", Method = "GET"  },
                // KeyClientName_user3
                new { Id = idCounter++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Equipment/Brand", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Well/WellMaster", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Well/WellMaster", Method = "POST" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Well/WellTest", Method = "GET"  },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user3", Route = "/ApiGateway/Well/WellTest", Method = "POST"  },
                // KeyClientName_user4
                new { Id = idCounter++, KeyClientName = "KeyClientName_user4", Route = "/ApiGateway/Equipment/EquipmentMaster", Method = "GET" },
                new { Id = idCounter++, KeyClientName = "KeyClientName_user4", Route = "/ApiGateway/Equipment/Brand", Method = "GET" }
            };
        }
        public static dynamic[] Route()
        {
            int idCounter = 1;
            return new dynamic[]
            {
                new { Id = idCounter++, 
                    DownstreamPathTemplate = "/api/EquipmentMaster", 
                    DownstreamScheme = "https",
                    UpstreamPathTemplate = "/ApiGateway/Equipment/EquipmentMaster",
                },
                new { Id = idCounter++,
                    DownstreamPathTemplate = "/api/Brand",
                    DownstreamScheme = "https",
                    UpstreamPathTemplate = "/ApiGateway/Equipment/Brand",
                },
                new { Id = idCounter++,
                    DownstreamPathTemplate = "/api/WellMaster",
                    DownstreamScheme = "https",
                    UpstreamPathTemplate = "/ApiGateway/Well/WellMaster",
                },
                new { Id = idCounter++,
                    DownstreamPathTemplate = "/api/WellTest",
                    DownstreamScheme = "https",
                    UpstreamPathTemplate = "/ApiGateway/Well/WellTest",
                },
            };
        }
        public static dynamic[] DownstreamHostAndPort()
        {
            int idCounter = 1;
            return new dynamic[]
            {
                new { Id = idCounter++, RouteId = 1, Host = "localhost", Port = "7074" },
                new { Id = idCounter++, RouteId = 2, Host = "localhost", Port = "7074" },
                new { Id = idCounter++, RouteId = 3, Host = "localhost", Port = "7242" },
                new { Id = idCounter++, RouteId = 4, Host = "localhost", Port = "7242" },
            };
        }
    }
}
