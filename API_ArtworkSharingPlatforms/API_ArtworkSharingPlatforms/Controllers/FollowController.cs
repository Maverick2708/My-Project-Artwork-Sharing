using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ArtworkSharingPlatforms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowController : Controller
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            this._followService = followService;
        }

        [HttpPost("CreateFollow")]
        public async Task<IActionResult> CreateFollow(CreateFollow createFollow)
        {
            var response = await _followService.CreateFollow(createFollow);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpDelete("UnFollow")]
        public async Task<IActionResult> UnFollow(string userId, string userIdFollow)
        {
            var response = await _followService.UnFollow(userId,userIdFollow);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("CountFollow")]
        public async Task<IActionResult> CountFollow(string userIdFollow)
        {
            var response = await _followService.CountFollow(userIdFollow);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("GetFollow")]
        public async Task<IActionResult> GetFollow(string userId, string userIdFollow)
        {
            var response = await _followService.GetFollow(userId, userIdFollow);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
    }
}
