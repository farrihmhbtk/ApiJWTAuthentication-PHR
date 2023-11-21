using ApiGatewayOcelot.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGatewayOcelot.Controllers
{
    public class RouteController : Controller
    {
        public IActionResult Index()
        {
            var _Route = UserManagement.Route();
            var _DownstreamHostAndPort = UserManagement.DownstreamHostAndPort();
            ViewData["Route"] = _Route;
            ViewData["DownstreamHostAndPort"] = _DownstreamHostAndPort;
            return View();
        }
    }
}
