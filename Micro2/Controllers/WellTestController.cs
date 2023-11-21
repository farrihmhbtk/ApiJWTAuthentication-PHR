using Micro2.Config;
using Micro2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micro2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellTestController : ControllerBase
    {
        private readonly Context _context;
        public WellTestController(Context context)
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
        public async Task<ActionResult<IEnumerable<WellTest>>> ShowAll()
        {
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            return await _context.WellTest.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<WellTest>> Add(WellTest _wellTest)
        {
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            _context.WellTest.Add(_wellTest);
            await _context.SaveChangesAsync();
            return Ok("Add WellTest Success.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            var _wellTest = await _context.WellTest.FindAsync(id);
            if (_wellTest == null)
            {
                return NotFound();
            }
            _context.WellTest.Remove(_wellTest);
            await _context.SaveChangesAsync();
            return Ok("Delete WellTest Success.");
        }
    }
}
