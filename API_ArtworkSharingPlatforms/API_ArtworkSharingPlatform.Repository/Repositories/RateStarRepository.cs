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
    public class RateStarRepository : IRatestarRepository
    {
        private readonly Artwork_SharingContext _context;

        public RateStarRepository(Artwork_SharingContext context)
        {
            this._context = context;
        }
        public async Task<ResponeModel> CreateRatestar(CreateRateStar model)
        {
            try
            {
                var rateStar = new RateStar
                {
                    UserId = model.UserId,
                    ArtworkPId = model.ArtworkPId,
                    Rate = model.Rate,
                };
                _context.RateStars.Add(rateStar);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Created Rate successfully", DataObject = rateStar };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while Creating the rate" };
            }
        }
        public async Task<ResponeModel> GetRateByArtworkID(int artworkID)
        {
            var ArtworkId = await _context.RateStars.Where(c => c.ArtworkPId == artworkID).ToListAsync();
            if (ArtworkId.Count > 0)
            {
                return new ResponeModel { Status = "Success", Message = "Rate found", DataObject = ArtworkId };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Rate not found" };
            }
        }
        public async Task<ResponeModel> AverageRate(int artworkID)
        {
            var AverageStar = await _context.RateStars
                .Where(c => c.ArtworkPId == artworkID)
                .Select(c => c.Rate).ToListAsync();
            
            if (AverageStar.Count > 0)
            {
                double sum = (double) AverageStar.Sum();
                double Average = sum/ AverageStar.Count;
                return new ResponeModel { Status = "Success", Message = "Rate found", DataObject = Average };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Rate not found" };
            }
        }
        public async Task<ResponeModel> GetRateByUserID(string userID)
        {
            var UserId = await _context.RateStars.Where(c => c.UserId == userID).ToListAsync();
            if (UserId.Count > 0)
            {
                return new ResponeModel { Status = "Success", Message = "Rate found", DataObject = UserId };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Rate not found" };
            }
        }
        public async Task<ResponeModel> UpdateRateStar(UpdateRateStarModel updateRate, int rateID)
        {
            try
            {
                var existingArtwork = await _context.RateStars.FirstOrDefaultAsync(a => a.RateId == rateID);

                if (existingArtwork == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Rate not found" };
                }

                existingArtwork = submitRateChanges(existingArtwork, updateRate);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Rate updated successfully", DataObject = existingArtwork };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updating the Rate" };
            }
        }
        private RateStar submitRateChanges(RateStar rateStar, UpdateRateStarModel updateRate)
        {
            rateStar.Rate = updateRate.rate;
            return rateStar;
        }
        public async Task<ResponeModel> GetRateByRateID(int RateID)
        {
            var rateId = await _context.RateStars.Where(c => c.RateId == RateID).ToListAsync();
            if (rateId.Count > 0)
            {
                return new ResponeModel { Status = "Success", Message = "Rate found", DataObject = rateId };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Rate not found" };
            }
        }
        public async Task<ResponeModel> AverageRateArtist(string UserID)
        {
            var artworkIds = await _context.Artworks
               .Where(c => c.UserId == UserID)
               .Select(c => c.ArtworkPId)
               .ToListAsync();

            if (artworkIds.Any())
            {
                var averageRatings = new List<double>();

                foreach (var artworkId in artworkIds)
                {
                    var averageStar = await _context.RateStars
                        .Where(c => c.ArtworkPId == artworkId)
                        .Select(c => c.Rate)
                        .ToListAsync();

                    if (averageStar.Any())
                    {
                        double sum = (double)averageStar.Sum();
                        double average = sum / averageStar.Count;
                        averageRatings.Add(average);
                    }
                }

                if (averageRatings.Any())
                {
                    double userAverage = averageRatings.Sum() / averageRatings.Count;
                    return new ResponeModel { Status = "Success", Message = "Rate found", DataObject = userAverage };
                }
                else
                {
                    return new ResponeModel { Status = "Error", Message = "Rate not found" };
                }
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Not found Post by this Artist" };
            }
        }
    }
}
