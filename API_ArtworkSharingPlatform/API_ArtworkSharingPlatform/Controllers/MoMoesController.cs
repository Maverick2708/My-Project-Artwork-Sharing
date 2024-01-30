using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ArtworkSharingPlatform.Entities;
using API_ArtworkSharingPlatform.ViewModels.MoMoViewModels;

namespace API_ArtworkSharingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoMoesController : ControllerBase
    {
        private readonly Artwork_SharingContext _context;

        public MoMoesController(Artwork_SharingContext context)
        {
            _context = context;
        }

        // GET: api/MoMoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MoMo>>> GetMoMos()
        {
          if (_context.MoMos == null)
          {
              return NotFound();
          }
            return await _context.MoMos.ToListAsync();
        }

        // GET: api/MoMoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MoMo>> GetMoMo(string id)
        {
          if (_context.MoMos == null)
          {
              return NotFound();
          }
            var moMo = await _context.MoMos.FindAsync(id);

            if (moMo == null)
            {
                return NotFound();
            }

            return moMo;
        }

        // PUT: api/MoMoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMoMo(string id, MoMo moMo)
        {
            if (id != moMo.PhoneMoMo)
            {
                return BadRequest();
            }

            _context.Entry(moMo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoMoExists(id))
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

        // POST: api/MoMoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MoMo>> PostMoMo(CreateMoMoDTO moMo)
        {
          if (_context.MoMos == null)
          {
              return Problem("Entity set 'Artwork_SharingContext.MoMos'  is null.");
          }
            var MoMo = new MoMo
            {
                PhoneMoMo = moMo.PhoneMoMo,
                NameMoMo = moMo.NameMoMo,
                Active = moMo.Active,
            };
            _context.MoMos.Add(MoMo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MoMoExists(moMo.PhoneMoMo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMoMo", new { id = moMo.PhoneMoMo }, moMo);
        }

        // DELETE: api/MoMoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMoMo(string id)
        {
            if (_context.MoMos == null)
            {
                return NotFound();
            }
            var moMo = await _context.MoMos.FindAsync(id);
            if (moMo == null)
            {
                return NotFound();
            }

            _context.MoMos.Remove(moMo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MoMoExists(string id)
        {
            return (_context.MoMos?.Any(e => e.PhoneMoMo == id)).GetValueOrDefault();
        }
    }
}
