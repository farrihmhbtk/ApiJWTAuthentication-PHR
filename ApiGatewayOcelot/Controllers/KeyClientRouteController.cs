using ApiGatewayOcelot.Config;
using ApiGatewayOcelot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

namespace ApiGatewayOcelot.Controllers
{
    public class KeyClientRouteController : Controller
    {
        private readonly Context _context;
        public KeyClientRouteController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            dynamic[] keyClientData = _context.GetKeyClientData().OrderBy(kc => kc.Id).ToArray();
            dynamic[] keyClientRouteData = _context.GetKeyClientRouteData().OrderBy(kcr => kcr.Id).ToArray();

            ViewData["KeyClientData"] = keyClientData;
            ViewData["keyClientRouteData"] = keyClientRouteData;
            return View();
        }
        [HttpPost]
        public IActionResult Create(int Id, string KeyClientName, string Route, string Method)
        {
            

            if (_context.KeyClientRoute.Any(kcr => kcr.Id == Id))
            {
                TempData["ErrorMessage"] = "Data with the same ID already exists!";
                return RedirectToAction("Index");
            }

            var keyClientRoute = new KeyClientRoute
            {
                Id = Id,
                KeyClientName = KeyClientName,
                Route = Route,
                Method = Method,
            };

            _context.KeyClientRoute.Add(keyClientRoute);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var keyClientRoute = _context.KeyClientRoute.Find(id);
            if (keyClientRoute == null)
            {
                TempData["ErrorMessage"] = "Data with the same ID does not exist!";
                return RedirectToAction("Index");
            }

            _context.KeyClientRoute.Remove(keyClientRoute);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
