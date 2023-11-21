using Micro1.Config;
using Micro1.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Micro1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EquipmentMasterController : ControllerBase
    {
        private readonly Context _context;
        public EquipmentMasterController(Context context)
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
        public async Task<ActionResult<IEnumerable<EquipmentMaster>>> ShowAll()
        {
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            return await _context.EquipmentMaster.ToListAsync();
        }
        [HttpPost]
        public async Task<ActionResult<EquipmentMaster>> Add(EquipmentMaster equipmentMaster)
        {
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            _context.EquipmentMaster.Add(equipmentMaster);
            await _context.SaveChangesAsync();

            return Ok("Add EquipmentMaster Success.");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string cek_PHRAPPKEYOcelot = HttpContext.Request.Headers["PHRKEYOcelot"];
            if (cek_PHRAPPKEYOcelot != GetPHRAPPKEYOcelot())
            {
                return BadRequest("PHRKEYOcelot Not Found or Incorrect.");
            }
            var equipmentMaster = await _context.EquipmentMaster.FindAsync(id);
            if (equipmentMaster == null)
            {
                return NotFound();
            }
            _context.EquipmentMaster.Remove(equipmentMaster);
            await _context.SaveChangesAsync();

            return Ok("Delete EquipmentMaster Success.");
        }
    }
}
