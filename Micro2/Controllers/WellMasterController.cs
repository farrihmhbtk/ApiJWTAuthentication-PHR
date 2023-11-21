using Micro2.Config;
using Micro2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micro2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WellMasterController : ControllerBase
    {
        private readonly Context _context;
        public WellMasterController(Context context)
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
        public async Task<ActionResult<IEnumerable<WellMaster>>> ShowAll()
        {
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            return await _context.WellMaster.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<WellMaster>> Add(WellMaster _wellMaster)
        {
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            _context.WellMaster.Add(_wellMaster);
            await _context.SaveChangesAsync();
            return Ok("Add WellMaster Success.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            var _wellMaster = await _context.WellMaster.FindAsync(id);
            if (_wellMaster == null)
            {
                return NotFound();
            }
            _context.WellMaster.Remove(_wellMaster);
            await _context.SaveChangesAsync();
            return Ok("Delete WellMaster Success.");
        }
    }
}
