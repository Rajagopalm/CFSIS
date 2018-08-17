using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CFSIS.Shared.Models;

namespace CFSIS.Server.Controllers
{
    [Produces("application/json")]
    [Route("api/Districts")]
    public class DistrictsController : Controller
    {
        private readonly CFSISContext _context;

        public DistrictsController(CFSISContext context)
        {
            _context = context;
        }

        // GET: api/Districts
        [HttpGet]
        public IEnumerable<Districts> GetDistricts()
        {
            return _context.Districts;
        }

        // GET: api/Districts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDistricts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var districts = await _context.Districts.SingleOrDefaultAsync(m => m.DistrictId == id);

            if (districts == null)
            {
                return NotFound();
            }

            return Ok(districts);
        }

        // PUT: api/Districts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDistricts([FromRoute] int id, [FromBody] Districts districts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != districts.DistrictId)
            {
                return BadRequest();
            }

            _context.Entry(districts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DistrictsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Districts
        [HttpPost]
        public async Task<IActionResult> PostDistricts([FromBody] Districts districts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Districts.Add(districts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDistricts", new { id = districts.DistrictId }, districts);
        }

        // DELETE: api/Districts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDistricts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var districts = await _context.Districts.SingleOrDefaultAsync(m => m.DistrictId == id);
            if (districts == null)
            {
                return NotFound();
            }

            _context.Districts.Remove(districts);
            await _context.SaveChangesAsync();

            return Ok(districts);
        }

        private bool DistrictsExists(int id)
        {
            return _context.Districts.Any(e => e.DistrictId == id);
        }
    }
}