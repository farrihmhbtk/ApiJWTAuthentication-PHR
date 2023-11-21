using ApiGatewayOcelot.Config;
using ApiGatewayOcelot.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiGatewayOcelot.Controllers
{
    public class HomeController : Controller
    {
        private readonly Context _context;
        public HomeController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
