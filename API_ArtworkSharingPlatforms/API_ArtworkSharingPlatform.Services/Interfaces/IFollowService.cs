using API_ArtworkSharingPlatform.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Interfaces
{
    public interface IFollowService
    {
        public Task<ResponeModel> CreateFollow(CreateFollow createFollow);
        public Task<ResponeModel> UnFollow(string userId, string userIdFollow);
        public Task<ResponeModel> CountFollow(string userIdFollow);
        public Task<ResponeModel> GetFollow(string userId, string userIdFollow);
    }
}
