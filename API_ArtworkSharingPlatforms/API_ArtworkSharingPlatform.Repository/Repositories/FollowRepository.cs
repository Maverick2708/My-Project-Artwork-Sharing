using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Repositories
{
    public class FollowRepository : IFollowRepository
    {
        private readonly Artwork_SharingContext _context;

        public FollowRepository(Artwork_SharingContext context)
        {
            this._context = context;
        }
        public async Task<ResponeModel> CreateFollow(CreateFollow createFollow)
        {
            try
            {
                var follow = new Follow
                {
                    UserId = createFollow.UserId,
                    UserIdFollow = createFollow.UserIdFollow,
                    DateFollow = DateTime.Now,
                    Status = true,
                };
                _context.Follows.Add(follow);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Created Follow successfully", DataObject = follow };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while Creating the follow" };
            }
        }

        public async Task<ResponeModel> UnFollow(string userId, string userIdFollow)
        {
            try
            {
                var existingFollow = await _context.Follows.FirstOrDefaultAsync(a => a.UserId == userId && a.UserIdFollow== userIdFollow);

                if (existingFollow == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Follow not found" };
                }
                _context.Follows.Remove(existingFollow);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Follow deleted successfully", DataObject = existingFollow };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while detele Follow" };
            }
        }
        public async Task<ResponeModel> CountFollow(string userIdFollow)
        {
            var followByUser = await _context.Follows
                .Where(c => c.UserIdFollow==userIdFollow).ToListAsync();
            if (followByUser.Count > 0)
            {
                int SumFollow = followByUser.Count();
                return new ResponeModel { Status = "Success", Message = "Follow found", DataObject = SumFollow };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Follow not found" };
            }
        }
        public async Task<ResponeModel> GetFollow(string userId, string userIdFollow)
        {
            var foundFollow = await _context.Follows
               .Where(c => c.UserId == userId && c.UserIdFollow==userIdFollow)
               .ToListAsync();
            bool followOrUnfollow = false;
            if (foundFollow.Count > 0)
            {
                followOrUnfollow = true;
                return new ResponeModel { Status = "Success", Message = "Follow found", DataObject = followOrUnfollow };
            }
            else
            {
                return new ResponeModel { Status = "Success", Message = "Follow not found", DataObject = followOrUnfollow };
            }
        }
    }
}
