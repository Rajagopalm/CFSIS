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

        /* GET: api/SubDistricts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetSubDistricts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subDistricts = await _context.SubDistricts.SingleOrDefaultAsync(m => m.SubDistrictsId == id);

            if (subDistricts == null)
            {
                return NotFound();
            }

            return Ok(subDistricts);
        }*/

        // GET: api/OrderDetails/5
        [HttpGet("{id}")]
        public IEnumerable<SubDistricts> GetSubDistricts([FromRoute] int id)
        {
            var subDistricts = _context.SubDistricts.Where(i => i.SubDistrictsId == id).ToList();
            return subDistricts;

        }

        // PUT: api/SubDistricts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSubDistricts([FromRoute] int id, [FromBody] SubDistricts subDistricts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != subDistricts.SubDistrictsId)
            {
                return BadRequest();
            }

            _context.Entry(subDistricts).State = EntityState.Modified;

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
        public async Task<IActionResult> PostSubDistricts([FromBody] SubDistricts subDistricts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.SubDistricts.Add(subDistricts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSubDistricts", new { id = subDistricts.SubDistrictsId }, subDistricts);
        }

        // DELETE: api/SubDistricts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSubDistricts([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var subDistricts = await _context.SubDistricts.SingleOrDefaultAsync(m => m.SubDistrictsId == id);
            if (subDistricts == null)
            {
                return NotFound();
            }

            _context.SubDistricts.Remove(subDistricts);
            await _context.SaveChangesAsync();

            return Ok(subDistricts);
        }

        private bool SubDistrictsExists(int id)
        {
            return _context.SubDistricts.Any(e => e.SubDistrictsId == id);
        }
    }
}