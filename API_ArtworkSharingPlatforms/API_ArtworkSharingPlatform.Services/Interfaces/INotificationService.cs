using API_ArtworkSharingPlatform.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Interfaces
{
    public interface INotificationService
    {
        public Task<ResponeModel> CreateNotification(CreateNotificationModel createNoti);
       // public Task<ResponeModel> CreateNotificationForFollow(CreateNotificationForContentModel createNoti);
        public Task<ResponeModel> GetNotiByUserId(string userId);
        public Task<ResponeModel> CountNotiByUserRe(string userId);
        public Task<ResponeModel> DeleteNoti(int notiId);
    }
}
