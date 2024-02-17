using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ArtworkSharingPlatforms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RequestController : Controller
    {
        private readonly IRequestService _requestService;

        public RequestController(IRequestService requestService)
        {
            this._requestService = requestService;
        }

        [HttpPost("CreateRequest")]
        public async Task<IActionResult> CreateRequest(CreateRequestModel createRequestModel)
        {
            var response = await _requestService.CreateRequest(createRequestModel);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllRequest")]
        public async Task<IActionResult> GetAllRequest()
        {
            var response = await _requestService.GetAllRequest();
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("CountRequest")]
        public async Task<IActionResult> CountRequest()
        {
            var response = await _requestService.CountRequest();
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
        [HttpPut("UpdateStatusRequest")]
        public async Task<IActionResult> UpdateStatusRequest(int requestId)
        {
            var response = await _requestService.UpdateStatusRequest(requestId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
    }
}
