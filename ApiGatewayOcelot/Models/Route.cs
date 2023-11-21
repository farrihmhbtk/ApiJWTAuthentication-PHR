using System.Collections.Generic;
namespace ApiGatewayOcelot.Models
{
    public class Route
    {
        public string DownstreamPathTemplate { get; set; }
        public string DownstreamScheme { get; set; }
        public List<HostAndPort> DownstreamHostAndPorts { get; set; }
        public string UpstreamPathTemplate { get; set; }
        public List<string> UpstreamHttpMethod { get; set; }
        public Dictionary<string, string> UpstreamHeaderTransform { get; set; }
    }

    public class HostAndPort
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }

    public class RouteData
    {
        public List<Route> Routes { get; set; }
    }

    public static class RouteDataFactory
    {
        public static RouteData CreateRouteData()
        {
            return new RouteData
            {
                Routes = new List<Route>
            {
                new Route
                {
                    DownstreamPathTemplate = "/api/EquipmentMaster",
                    DownstreamScheme = "https",
                    DownstreamHostAndPorts = new List<HostAndPort>
                    {
                        new HostAndPort
                        {
                            Host = "localhost",
                            Port = 7074
                        }
                    },
                    UpstreamPathTemplate = "/ApiGateway/Equipment/EquipmentMaster",
                    UpstreamHttpMethod = new List<string> { "Get", "Post" },
                    UpstreamHeaderTransform = new Dictionary<string, string>
                    {
                        { "PHRKEYOcelot", "API123" }
                    }
                },
                // Tambahkan entri lain sesuai dengan format yang sama
            }
            };
        }
    }

}
