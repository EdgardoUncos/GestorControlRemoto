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

namespace GestorControlRemoto.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ControlRemotoController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ControlRemotoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ControlRemoto
        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<ControlRemoto>>> GetControlRemoto()
        {
            if (_context == null || _context.ControlRemoto == null)
            {
                return NotFound();
            }

            try
            {
                var controlRemotos = await _context.ControlRemoto.Include(o => o.Compatible).ToListAsync();
                return Ok(controlRemotos);
            }
            catch (Exception ex)
            {
                // Log the exception (using a logging framework, e.g., Serilog, NLog, etc.)
                // _logger.LogError(ex, "An error occurred while fetching ControlRemotos.");

                // Return a generic error message
                return StatusCode(500, "Internal server error");
            }
        }

        // GET: api/ControlRemoto/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<ControlRemoto>> GetControlRemoto(int id)
        {
            if (_context.ControlRemoto == null)
            {
                return NotFound();
            }
            var controlRemoto = await _context.ControlRemoto.FindAsync(id);

            if (controlRemoto == null)
            {
                return NotFound();
            }

            return controlRemoto;
        }

        // PUT: api/ControlRemoto/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutControlRemoto(int id, ControlRemoto controlRemoto)
        {
            if (id != controlRemoto.Id)
            {
                return BadRequest();
            }

            _context.Entry(controlRemoto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ControlRemotoExists(id))
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

        // POST: api/ControlRemoto
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ControlRemoto>> PostControlRemoto(ControlRemoto controlRemoto)
        {
            if (_context.ControlRemoto == null)
            {
                return Problem("Entity set 'ApplicationDbContext.ControlRemoto'  is null.");
            }
            _context.ControlRemoto.Add(controlRemoto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetControlRemoto", new { id = controlRemoto.Id }, controlRemoto);
        }

        // DELETE: api/ControlRemoto/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteControlRemoto(int id)
        {
            if (_context.ControlRemoto == null)
            {
                return NotFound();
            }
            var controlRemoto = await _context.ControlRemoto.FindAsync(id);
            if (controlRemoto == null)
            {
                return NotFound();
            }

            _context.ControlRemoto.Remove(controlRemoto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ControlRemotoExists(int id)
        {
            return (_context.ControlRemoto?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
