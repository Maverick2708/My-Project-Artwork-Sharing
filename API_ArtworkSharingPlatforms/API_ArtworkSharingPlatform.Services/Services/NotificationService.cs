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
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(INotificationRepository notificationRepository)
        {
            this._notificationRepository = notificationRepository;
        }
        public async Task<ResponeModel> CreateNotification(CreateNotificationModel createNoti)
        {
            return await _notificationRepository.CreateNotification(createNoti);
        }
        //public async Task<ResponeModel> CreateNotificationForFollow(CreateNotificationForContentModel createNoti)
        //{
        //    return await _notificationRepository.CreateNotificationForFollow(createNoti);
        //}
        public async Task<ResponeModel> GetNotiByUserId(string userId)
        {
            return await _notificationRepository.GetNotiByUserId(userId);
        }
        public async Task<ResponeModel> CountNotiByUserRe(string userId)
        {
            return await _notificationRepository.CountNotiByUserRe(userId);
        }
        public async Task<ResponeModel> DeleteNoti(int notiId)
        {
            return await _notificationRepository.DeleteNoti(notiId);
        }
    }
}
