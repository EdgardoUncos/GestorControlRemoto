using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GestorControlRemoto.Shared.Data;
using GestorControlRemoto.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Permissions;

namespace GestorControlRemoto.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CompatiblesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CompatiblesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Compatibles
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<Compatible>>> GetCompatible()
        {
          if (_context.Compatible == null)
          {
              return NotFound();
          }
            return await _context.Compatible.ToListAsync();
        }

        // GET: api/Compatibles/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<Compatible>> GetCompatible(int id)
        {
          if (_context.Compatible == null)
          {
              return NotFound();
          }
            var compatible = await _context.Compatible.FindAsync(id);

            if (compatible == null)
            {
                return NotFound();
            }

            return compatible;
        }

        // PUT: api/Compatibles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCompatible(int id, Compatible compatible)
        {
            if (id != compatible.Id)
            {
                return BadRequest();
            }

            _context.Entry(compatible).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CompatibleExists(id))
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

        // POST: api/Compatibles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Compatible>> PostCompatible(Compatible compatible)
        {
          if (_context.Compatible == null)
          {
              return Problem("Entity set 'ApplicationDbContext.Compatible'  is null.");
          }
            _context.Compatible.Add(compatible);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCompatible", new { id = compatible.Id }, compatible);
        }

        // DELETE: api/Compatibles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCompatible(int id)
        {
            if (_context.Compatible == null)
            {
                return NotFound();
            }
            var compatible = await _context.Compatible.FindAsync(id);
            if (compatible == null)
            {
                return NotFound();
            }

            _context.Compatible.Remove(compatible);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CompatibleExists(int id)
        {
            return (_context.Compatible?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
