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
    public class NotificationRepository : INotificationRepository
    {
        private readonly Artwork_SharingContext _context;

        public NotificationRepository(Artwork_SharingContext context)
        {
            this._context = context;
        }
        public async Task<ResponeModel> CreateNotification(CreateNotificationModel createNoti)
        {
            try
            {
                var fullName = await _context.People
                    .Where(c=>c.Id == createNoti.UserId)
                    .Select(x => x.FullName).FirstOrDefaultAsync();
                if (fullName == null)
                {
                    return new ResponeModel { Status = "Error", Message = "User not found" };
                }
                else
                {
                    var Noti = new Notification
                    {
                        UserId = createNoti.UserId,
                        ContentNoti = createNoti.ContentNoti,
                        DateNoti = DateTime.Now,
                        UserIdReceive = createNoti.UserIdReceive,
                        Status = true,
                    };
                    _context.Notifications.Add(Noti);
                    await _context.SaveChangesAsync();

                    return new ResponeModel { Status = "Success", Message = "Created Notification successfully", DataObject = Noti };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while Creating the notification" };
            }
        }
        //public async Task<ResponeModel> CreateNotificationForFollow(CreateNotificationForContentModel createNoti)
        //{
        //    try
        //    {
        //        var fullName = await _context.People
        //            .Where(c => c.Id == createNoti.UserId)
        //            .Select(x => x.FullName).FirstOrDefaultAsync();
        //        if (fullName == null)
        //        {
        //            return new ResponeModel { Status = "Error", Message = "User not found" };
        //        }
        //        else
        //        {
        //            var Noti = new Notification
        //            {
        //                UserId = createNoti.UserId,
        //                ContentNoti = $"{fullName} - liked the article",
        //                DateNoti = DateTime.Now,
        //                Status = true,
        //            };
        //            _context.Notifications.Add(Noti);
        //            await _context.SaveChangesAsync();

        //            return new ResponeModel { Status = "Success", Message = "Created Notification successfully", DataObject = Noti };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine($"Exception: {ex.Message}");
        //        return new ResponeModel { Status = "Error", Message = "An error occurred while Creating the notification" };
        //    }
        //}
        public async Task<ResponeModel> GetNotiByUserId(string userId)
        {
            var foundNoti = await _context.Notifications
               .Where(c => c.UserIdReceive == userId && c.Status == true)
               .ToListAsync();
            //bool followOrUnfollow = false;
            if (foundNoti.Count > 0)
            {
                //followOrUnfollow = true;
                return new ResponeModel { Status = "Success", Message = "Found Notification", DataObject = foundNoti };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Notification not found" };
            }
        }

        public async Task<ResponeModel> CountNotiByUserRe(string userId)
        {
            var NotiByuser = await _context.Notifications
                .Where(c => c.UserIdReceive == userId && c.Status==true).ToListAsync();
            if (NotiByuser.Count > 0)
            {
                int SumNoti = NotiByuser.Count();
                return new ResponeModel { Status = "Success", Message = "Notification found", DataObject = SumNoti };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Notification not found" };
            }
        }
        public async Task<ResponeModel> DeleteNoti(int notiId)
        {
            try
            {
                var existingNoti = await _context.Notifications.FirstOrDefaultAsync(a => a.NotificationId == notiId && a.Status==true);

                if (existingNoti == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Notification not found" };
                }

                existingNoti = ChangeStatusOfNotification(existingNoti);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Notification deleted successfully", DataObject = existingNoti };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while detele Notification" };
            }
        }
        private Notification ChangeStatusOfNotification(Notification notification)
        {
            notification.Status = false;
            return notification;
        }
    }
}
