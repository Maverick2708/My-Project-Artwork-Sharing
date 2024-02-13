using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ArtworkSharingPlatforms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RateStarController : Controller
    {
        private readonly IRatestarService _ratestarService;

        public RateStarController(IRatestarService ratestarService)
        {
            this._ratestarService = ratestarService;
        }

        [HttpPost("CreateRateStar")]
        public async Task<IActionResult> CreateRatestar(CreateRateStar rateStar)
        {
            var response = await _ratestarService.CreateRatestar(rateStar);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("GetRateByArtworkID")]
        public async Task<IActionResult> GetRateByArtworkID(int artworkID)
        {
            var response = await _ratestarService.GetRateByArtworkID(artworkID);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
        [HttpGet("GetAverageRateStar")]
        public async Task<IActionResult> AverageRate(int artworkID)
        {
            var response = await _ratestarService.AverageRate(artworkID);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("GetRateByUserID")]
        public async Task<IActionResult> GetRateByUserID(string userID)
        {
            var response = await _ratestarService.GetRateByUserID(userID);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateRateStar")]
        public async Task<IActionResult> UpdateRateStar(UpdateRateStarModel updateRate, int rateID)
        {
            var artwork = await _ratestarService.GetRateByRateID(rateID);
            var response = await _ratestarService.UpdateRateStar(updateRate, rateID);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }

            return Ok(response);
        }

        [HttpGet("GetAverageRateStarArtist")]
        public async Task<IActionResult> AverageRateArtist(string UserID)
        {
            var response = await _ratestarService.AverageRateArtist(UserID);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
    }
}
