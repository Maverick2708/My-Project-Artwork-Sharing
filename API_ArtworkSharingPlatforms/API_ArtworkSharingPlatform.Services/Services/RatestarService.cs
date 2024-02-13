using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Repository.Repositories;
using API_ArtworkSharingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Services
{
    public class RatestarService: IRatestarService
    {
        private readonly IRatestarRepository _ratestarRepository;

        public RatestarService(IRatestarRepository ratestarRepository)
        {
            this._ratestarRepository = ratestarRepository;
        }
        public async Task<ResponeModel> CreateRatestar(CreateRateStar rateStar)
        {
            return await _ratestarRepository.CreateRatestar(rateStar);
        }
        public async Task<ResponeModel> GetRateByArtworkID(int artworkID)
        {
            return await _ratestarRepository.GetRateByArtworkID(artworkID);
        }
        public async Task<ResponeModel> AverageRate(int artworkID)
        {
            return await _ratestarRepository.AverageRate(artworkID);
        }
        public async Task<ResponeModel> GetRateByUserID(string userID)
        {
            return await _ratestarRepository.GetRateByUserID(userID);
        }
        public async Task<ResponeModel> UpdateRateStar(UpdateRateStarModel updateRate, int rateID)
        {
            return await _ratestarRepository.UpdateRateStar(updateRate,rateID);
        }
        public async Task<ResponeModel> GetRateByRateID(int RateID)
        {
            return await _ratestarRepository.GetRateByRateID(RateID);
        }
        public async Task<ResponeModel> AverageRateArtist(string UserID)
        {
            return await _ratestarRepository.AverageRateArtist(UserID);
        }
    }
}
