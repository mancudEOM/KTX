using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KTX.Models;

namespace KTX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoryRentsController : ControllerBase
    {
        private readonly KtxDbContext _context;

        public HistoryRentsController(KtxDbContext context)
        {
            _context = context;
        }

        // GET: api/HistoryRents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryRent>>> GetHistoryRents()
        {
            if (_context.HistoryRents == null)
            {
                return NotFound();
            }
            return await _context.HistoryRents.ToListAsync();
        }

        // GET: api/HistoryRents/5
        [HttpGet("{id}")]
        public ActionResult<HistoryRent> GetHistoryRent(int id)
        {
            if (_context.HistoryRents == null)
            {
                return NotFound();
            }
            var historyRent = _context.HistoryRents.Where(i => i.Id == id).Include(x => x.Rents).FirstOrDefault();

            if (historyRent == null)
            {
                return NotFound();
            }

            return historyRent;
        }

        // PUT: api/HistoryRents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryRent(int id, HistoryRent historyRent)
        {
            if (id != historyRent.Id)
            {
                return BadRequest();
            }

            _context.Entry(historyRent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryRentExists(id))
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

        // POST: api/HistoryRents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoryRent>> PostHistoryRent(HistoryRent historyRent)
        {
            if (_context.HistoryRents == null)
            {
                return Problem("Entity set 'DormDbContext.HistoryRents'  is null.");
            }
            _context.HistoryRents.Add(historyRent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistoryRent", new { id = historyRent.Id }, historyRent);
        }

        // DELETE: api/HistoryRents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryRent(int id)
        {
            if (_context.HistoryRents == null)
            {
                return NotFound();
            }
            var historyRent = await _context.HistoryRents.FindAsync(id);
            if (historyRent == null)
            {
                return NotFound();
            }

            _context.HistoryRents.Remove(historyRent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoryRentExists(int id)
        {
            return (_context.HistoryRents?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}