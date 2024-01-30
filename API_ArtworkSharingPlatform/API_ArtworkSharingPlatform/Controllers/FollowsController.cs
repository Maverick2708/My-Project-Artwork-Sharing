﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API_ArtworkSharingPlatform.Entities;
using API_ArtworkSharingPlatform.ViewModels.FollowViewModels;

namespace API_ArtworkSharingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly Artwork_SharingContext _context;

        public FollowsController(Artwork_SharingContext context)
        {
            _context = context;
        }

        // GET: api/Follows
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Follow>>> GetFollows()
        {
          if (_context.Follows == null)
          {
              return NotFound();
          }
            return await _context.Follows.ToListAsync();
        }

        // GET: api/Follows/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Follow>> GetFollow(int id)
        {
          if (_context.Follows == null)
          {
              return NotFound();
          }
            var follow = await _context.Follows.FindAsync(id);

            if (follow == null)
            {
                return NotFound();
            }

            return follow;
        }

        // PUT: api/Follows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollow(int id, Follow follow)
        {
            if (id != follow.FollowId)
            {
                return BadRequest();
            }

            _context.Entry(follow).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowExists(id))
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

        // POST: api/Follows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Follow>> PostFollow(CreateFollowDTO follow)
        {
          if (_context.Follows == null)
          {
              return Problem("Entity set 'Artwork_SharingContext.Follows'  is null.");
          }
            var Follow = new Follow
            {
                FollowId = follow.FollowId,
                UserId = follow.UserId,
                UserIdFollow = follow.UserId,
                DateFollow = follow.DateFollow,
                Status = follow.Status,
            };
            _context.Follows.Add(Follow);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (FollowExists(follow.FollowId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetFollow", new { id = follow.FollowId }, follow);
        }

        // DELETE: api/Follows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollow(int id)
        {
            if (_context.Follows == null)
            {
                return NotFound();
            }
            var follow = await _context.Follows.FindAsync(id);
            if (follow == null)
            {
                return NotFound();
            }

            _context.Follows.Remove(follow);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FollowExists(int id)
        {
            return (_context.Follows?.Any(e => e.FollowId == id)).GetValueOrDefault();
        }
    }
}
