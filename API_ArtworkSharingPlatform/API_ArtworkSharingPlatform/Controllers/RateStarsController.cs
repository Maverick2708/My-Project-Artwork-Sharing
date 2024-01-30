using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ArtworkSharingPlatform.Entities;
using API_ArtworkSharingPlatform.ViewModels.RateStarViewModels;

namespace API_ArtworkSharingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateStarsController : ControllerBase
    {
        private readonly Artwork_SharingContext _context;

        public RateStarsController(Artwork_SharingContext context)
        {
            _context = context;
        }

        // GET: api/RateStars
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RateStar>>> GetRateStars()
        {
          if (_context.RateStars == null)
          {
              return NotFound();
          }
            return await _context.RateStars.ToListAsync();
        }

        // GET: api/RateStars/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RateStar>> GetRateStar(int id)
        {
          if (_context.RateStars == null)
          {
              return NotFound();
          }
            var rateStar = await _context.RateStars.FindAsync(id);

            if (rateStar == null)
            {
                return NotFound();
            }

            return rateStar;
        }

        // PUT: api/RateStars/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRateStar(int id, RateStar rateStar)
        {
            if (id != rateStar.RateId)
            {
                return BadRequest();
            }

            _context.Entry(rateStar).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RateStarExists(id))
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

        // POST: api/RateStars
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RateStar>> PostRateStar(CreateRateStarDTO rateStar)
        {
          if (_context.RateStars == null)
          {
              return Problem("Entity set 'Artwork_SharingContext.RateStars'  is null.");
          }
            var RateStar = new RateStar
            {
                RateId = rateStar.RateId,
                UserId = rateStar.UserId,
                ArtworkPId = rateStar.ArtworkPId,
                Rate = rateStar.Rate,
            };
            _context.RateStars.Add(RateStar);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (RateStarExists(rateStar.RateId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetRateStar", new { id = rateStar.RateId }, rateStar);
        }

        // DELETE: api/RateStars/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRateStar(int id)
        {
            if (_context.RateStars == null)
            {
                return NotFound();
            }
            var rateStar = await _context.RateStars.FindAsync(id);
            if (rateStar == null)
            {
                return NotFound();
            }

            _context.RateStars.Remove(rateStar);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RateStarExists(int id)
        {
            return (_context.RateStars?.Any(e => e.RateId == id)).GetValueOrDefault();
        }
    }
}
