using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ITICommunity.Models;

namespace ITICommunity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContactsController : ControllerBase
    {
        private readonly ITICommunityContext _context;

        public UserContactsController(ITICommunityContext context)
        {
            _context = context;
        }

        // GET: api/UserContacts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserContacts>>> GetUserContacts()
        {
            return await _context.UserContacts.ToListAsync();
        }

        // GET: api/UserContacts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserContacts>> GetUserContacts(int id)
        {
            var userContacts = await _context.UserContacts.FindAsync(id);

            if (userContacts == null)
            {
                return NotFound();
            }

            return userContacts;
        }

        // PUT: api/UserContacts/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserContacts(int id, UserContacts userContacts)
        {
            if (id != userContacts.Id)
            {
                return BadRequest();
            }

            _context.Entry(userContacts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserContactsExists(id))
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

        // POST: api/UserContacts
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserContacts>> PostUserContacts(UserContacts userContacts)
        {
            _context.UserContacts.Add(userContacts);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserContactsExists(userContacts.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUserContacts", new { id = userContacts.Id }, userContacts);
        }

        // DELETE: api/UserContacts/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserContacts>> DeleteUserContacts(int id)
        {
            var userContacts = await _context.UserContacts.FindAsync(id);
            if (userContacts == null)
            {
                return NotFound();
            }

            _context.UserContacts.Remove(userContacts);
            await _context.SaveChangesAsync();

            return userContacts;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool UserContactsExists(int id)
        {
            return _context.UserContacts.Any(e => e.Id == id);
        }
    }
}
