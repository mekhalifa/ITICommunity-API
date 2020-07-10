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
    public class IntakesController : ControllerBase
    {
        private readonly ITICommunityContext _context;

        public IntakesController(ITICommunityContext context)
        {
            _context = context;
        }

        // GET: api/Intakes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Intake>>> GetIntake()
        {
            return await _context.Intakes.ToListAsync();
        }

        // GET: api/Intakes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Intake>> GetIntake(int id)
        {
            var intake = await _context.Intakes.FindAsync(id);

            if (intake == null)
            {
                return NotFound();
            }

            return intake;
        }

        // PUT: api/Intakes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIntake(int id, Intake intake)
        {
            if (id != intake.Id)
            {
                return BadRequest();
            }

            _context.Entry(intake).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IntakeExists(id))
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

        // POST: api/Intakes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<Intake>> PostIntake(Intake intake)
        {
            _context.Intakes.Add(intake);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIntake", new { id = intake.Id }, intake);
        }

        // DELETE: api/Intakes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Intake>> DeleteIntake(int id)
        {
            var intake = await _context.Intakes.FindAsync(id);
            if (intake == null)
            {
                return NotFound();
            }

            _context.Intakes.Remove(intake);
            await _context.SaveChangesAsync();

            return intake;
        }
        [ApiExplorerSettings(IgnoreApi = true)]
        private bool IntakeExists(int id)
        {
            return _context.Intakes.Any(e => e.Id == id);
        }
    }
}
