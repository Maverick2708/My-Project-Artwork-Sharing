using API_ArtworkSharingPlatform.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Interfaces
{
    public interface IRatestarService
    {
        public Task<ResponeModel> CreateRatestar(CreateRateStar rateStar);
        public Task<ResponeModel> GetRateByArtworkID(int artworkID);
        public Task<ResponeModel> AverageRate(int artworkID);
        public Task<ResponeModel> AverageRateArtist(string UserID);
        public Task<ResponeModel> GetRateByUserID(string userID);
        public Task<ResponeModel> GetRateByRateID(int RateID);
        public Task<ResponeModel> UpdateRateStar(UpdateRateStarModel updateRate, int rateID);
    }
}
