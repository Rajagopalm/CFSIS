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
    [Route("api/SubDistricts")]
    public class SubDistrictsController : Controller
    {
        private readonly CFSISContext _context;

        public SubDistrictsController(CFSISContext context)
        {
            _context = context;
        }

        // GET: api/SubDistricts
        [HttpGet]
        public IEnumerable<SubDistricts> GetSubDistricts()
        {
            return _context.SubDistricts;
        }

        // GET: api/SubDistricts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubDistricts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subdistricts = await _context.SubDistricts.SingleOrDefaultAsync(m => m.SubDistrictsId == id);

            if (subdistricts == null)
            {
                return NotFound();
            }

            return Ok(subdistricts);
        }

        // PUT: api/SubDistricts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubDistricts([FromRoute] int id, [FromBody] SubDistricts subdistricts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subdistricts.SubDistrictsId)
            {
                return BadRequest();
            }

            _context.Entry(subdistricts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubDistrictsExists(id))
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

        // POST: api/SubDistricts
        [HttpPost]
        public async Task<IActionResult> PostSubDistricts([FromBody] SubDistricts subdistricts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SubDistricts.Add(subdistricts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubDistricts", new { id = subdistricts.SubDistrictsId }, subdistricts);
        }

        // DELETE: api/SubDistricts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubDistricts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subdistricts = await _context.SubDistricts.SingleOrDefaultAsync(m => m.SubDistrictsId == id);
            if (subdistricts == null)
            {
                return NotFound();
            }

            _context.SubDistricts.Remove(subdistricts);
            await _context.SaveChangesAsync();

            return Ok(subdistricts);
        }

        private bool SubDistrictsExists(int id)
        {
            return _context.SubDistricts.Any(e => e.SubDistrictsId == id);
        }
    }
}