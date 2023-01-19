using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Teatastic.Data;
using Teatastic.Models;

namespace Teatastic.APIControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeasController : ControllerBase
    {
        private readonly TeatasticContext _context;

        public TeasController(TeatasticContext context)
        {
            _context = context;
        }

        // GET: api/Teas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tea>>> GetTea()
        {
            return await _context.Tea.ToListAsync();
        }

        // GET: api/Teas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tea>> GetTea(int id)
        {
            var tea = await _context.Tea.FindAsync(id);

            if (tea == null)
            {
                return NotFound();
            }

            return tea;
        }

        // PUT: api/Teas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTea(int id, Tea tea)
        {
            if (id != tea.Id)
            {
                return BadRequest();
            }

            _context.Entry(tea).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeaExists(id))
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

        // POST: api/Teas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tea>> PostTea(Tea tea)
        {
            _context.Tea.Add(tea);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTea", new { id = tea.Id }, tea);
        }

        // DELETE: api/Teas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTea(int id)
        {
            var tea = await _context.Tea.FindAsync(id);
            if (tea == null)
            {
                return NotFound();
            }

            _context.Tea.Remove(tea);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TeaExists(int id)
        {
            return _context.Tea.Any(e => e.Id == id);
        }
    }
}
