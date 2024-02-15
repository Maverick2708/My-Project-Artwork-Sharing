using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowRepository _followRepository;

        public FollowService(IFollowRepository followRepository)
        {
            this._followRepository = followRepository;
        }
        public async Task<ResponeModel> CreateFollow(CreateFollow createFollow)
        {
            return await _followRepository.CreateFollow(createFollow);
        }
        public async Task<ResponeModel> UnFollow(string userId, string userIdFollow)
        {
            return await _followRepository.UnFollow(userId, userIdFollow);
        }
        public async Task<ResponeModel> CountFollow(string userIdFollow)
        {
            return await _followRepository.CountFollow(userIdFollow);
        }
        public async Task<ResponeModel> GetFollow(string userId, string userIdFollow)
        {
            return await _followRepository.GetFollow(userId, userIdFollow);
        }
    }
}
