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
    public class UserWorkExperiencesController : ControllerBase
    {
        private readonly ITICommunityContext _context;

        public UserWorkExperiencesController(ITICommunityContext context)
        {
            _context = context;
        }

        // GET: api/UserWorkExperiences
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserWorkExperience>>> GetUserWorkExperience()
        {
            return await _context.UserWorkExperiences.ToListAsync();
        }

        // GET: api/UserWorkExperiences/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserWorkExperience>> GetUserWorkExperience(int id)
        {
            var userWorkExperience = await _context.UserWorkExperiences.FindAsync(id);

            if (userWorkExperience == null)
            {
                return NotFound();
            }

            return userWorkExperience;
        }

        // PUT: api/UserWorkExperiences/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserWorkExperience(int id, UserWorkExperience userWorkExperience)
        {
            if (id != userWorkExperience.Id)
            {
                return BadRequest();
            }

            _context.Entry(userWorkExperience).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserWorkExperienceExists(id))
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

        // POST: api/UserWorkExperiences
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<UserWorkExperience>> PostUserWorkExperience(UserWorkExperience userWorkExperience)
        {
            _context.UserWorkExperiences.Add(userWorkExperience);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserWorkExperience", new { id = userWorkExperience.Id }, userWorkExperience);
        }

        // DELETE: api/UserWorkExperiences/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserWorkExperience>> DeleteUserWorkExperience(int id)
        {
            var userWorkExperience = await _context.UserWorkExperiences.FindAsync(id);
            if (userWorkExperience == null)
            {
                return NotFound();
            }

            _context.UserWorkExperiences.Remove(userWorkExperience);
            await _context.SaveChangesAsync();

            return userWorkExperience;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        private bool UserWorkExperienceExists(int id)
        {
            return _context.UserWorkExperiences.Any(e => e.Id == id);
        }
    }
}
