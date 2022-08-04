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
    public class RelativeUsersController : ControllerBase
    {
        private readonly KtxDbContext _context;

        public RelativeUsersController(KtxDbContext context)
        {
            _context = context;
        }

        // GET: api/RelativeUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RelativeUser>>> GetRelativeUsers()
        {
            if (_context.RelativeUsers == null)
            {
                return NotFound();
            }
            return await _context.RelativeUsers.ToListAsync();
        }

        // GET: api/RelativeUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RelativeUser>> GetRelativeUser(int id)
        {
            if (_context.RelativeUsers == null)
            {
                return NotFound();
            }
            var relativeUser = await _context.RelativeUsers.FindAsync(id);

            if (relativeUser == null)
            {
                return NotFound();
            }

            return relativeUser;
        }

        // PUT: api/RelativeUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRelativeUser(int id, RelativeUser relativeUser)
        {
            if (id != relativeUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(relativeUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RelativeUserExists(id))
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

        // POST: api/RelativeUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RelativeUser>> PostRelativeUser(RelativeUser relativeUser)
        {
            if (_context.RelativeUsers == null)
            {
                return Problem("Entity set 'DormDbContext.RelativeUsers'  is null.");
            }
            _context.RelativeUsers.Add(relativeUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRelativeUser", new { id = relativeUser.Id }, relativeUser);
        }

        // DELETE: api/RelativeUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRelativeUser(int id)
        {
            if (_context.RelativeUsers == null)
            {
                return NotFound();
            }
            var relativeUser = await _context.RelativeUsers.FindAsync(id);
            if (relativeUser == null)
            {
                return NotFound();
            }

            _context.RelativeUsers.Remove(relativeUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RelativeUserExists(int id)
        {
            return (_context.RelativeUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}