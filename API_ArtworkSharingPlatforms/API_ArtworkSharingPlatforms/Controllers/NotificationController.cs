using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ArtworkSharingPlatforms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            this._notificationService = notificationService;
        }

        [HttpPost("CreateNotification")]
        public async Task<IActionResult> CreateNotification(CreateNotificationModel createNoti)
        {
            var response = await _notificationService.CreateNotification(createNoti);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        //[HttpPost("CreateNotificationForFollow")]
        //public async Task<IActionResult> CreateNotificationForFollow(CreateNotificationForContentModel createNoti)
        //{
        //    var response = await _notificationService.CreateNotificationForFollow(createNoti);
        //    if (response.Status == "Error")
        //    {
        //        return Conflict(response);
        //    }
        //    return Ok(response);
        //}

        [HttpGet("GetNotiByUserId")]
        public async Task<IActionResult> GetNotiByUserId(string userId)
        {
            var response = await _notificationService.GetNotiByUserId(userId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("CountNotiByUserRe")]
        public async Task<IActionResult> CountNotiByUserRe(string userId)
        {
            var response = await _notificationService.CountNotiByUserRe(userId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPut("DeleteNoti")]
        public async Task<IActionResult> DeleteNoti(int notiId)
        {
            var response = await _notificationService.DeleteNoti(notiId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
    }
}
