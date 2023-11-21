using ApiGatewayOcelot.Config;
using ApiGatewayOcelot.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiGatewayOcelot.Controllers
{
    public class KeyClientController : Controller
    {
        private readonly Context _context;
        public KeyClientController(Context context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            dynamic[] keyClientData = _context.GetKeyClientData();
            ViewData["KeyClientData"] = keyClientData;

            return View();
        }
        [HttpPost]
        public IActionResult Create(int Id, string KeyClientName, string Status)
        {

            if (_context.KeyClient.Any(kc => kc.Id == Id))
            {
                TempData["ErrorMessage"] = "Data with the same ID and KeyClientName already exists!";
                return RedirectToAction("Index");
            }
            var keyClient = new KeyClient
            {
                Id = Id,
                KeyClientName = KeyClientName,
                Status = Status
            };

            _context.KeyClient.Add(keyClient);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Delete(int id)
        {
            var keyClient = _context.KeyClient.Find(id);
            if (keyClient == null)
            {
                TempData["ErrorMessage"] = "Data with the same ID does not exist!";
                return RedirectToAction("Index");
            }

            _context.KeyClient.Remove(keyClient);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
