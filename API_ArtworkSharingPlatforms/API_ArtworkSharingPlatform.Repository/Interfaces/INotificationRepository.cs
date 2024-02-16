using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Interfaces
{
    public interface INotificationRepository
    {
        public Task<ResponeModel> CreateNotification(CreateNotificationModel createNoti);
        //public Task<ResponeModel> CreateNotificationForFollow(CreateNotificationForContentModel createNoti);
        public Task<ResponeModel> GetNotiByUserId(string userId);
        public Task<ResponeModel> CountNotiByUserRe(string userId);
        public Task<ResponeModel> DeleteNoti(int notiId);
    }
}
