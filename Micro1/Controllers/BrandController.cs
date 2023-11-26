using Micro1.Config;
using Micro1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micro1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly Context _context;
        public BrandController(Context context)
        {
            _context = context;
        }
        private string GetPHRAPPKEYOcelot()
        {
            var sysConfigs = _context.SysConfig.ToList();
            var sysConfig = sysConfigs.FirstOrDefault(sysConfig => sysConfig.SysKey == "PHRAPPKEYOcelot");
            if (sysConfig != null)
            {
                return sysConfig.SysValue;
            }
            else
            {
                throw new InvalidOperationException("SysConfig with SysKey 'PHRAPPKEYOcelot' Not Found.");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brand>>> ShowAll()
        {
            
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            
            return await _context.Brand.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<Brand>> Add(Brand _brand)
        {
            
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            

            _context.Brand.Add(_brand);
            await _context.SaveChangesAsync();
            return Ok("Add Brand Success.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            
            var _brand = await _context.Brand.FindAsync(id);
            if (_brand == null)
            {
                return NotFound();
            }
            
            _context.Brand.Remove(_brand);
            await _context.SaveChangesAsync();
            return Ok("Delete Brand Success.");
        }
    }
}
