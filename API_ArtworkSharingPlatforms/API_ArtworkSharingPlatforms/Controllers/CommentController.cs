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
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            this._commentService = commentService;
        }

        [HttpPost("CreateComment")]
        public async Task<IActionResult> CreateComment(CreateComment createComment)
        {
            var response = await _commentService.CreateComment(createComment);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }


        [HttpPost("CreateInteract")]
        public async Task<IActionResult> CreateInteract(CreateInteract createInteract)
        {
            var response = await _commentService.CreateInteract(createInteract);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpDelete("DisLike")]
        public async Task<IActionResult> DisLike(int commentId)
        {
            var response = await _commentService.DisLike(commentId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPut("HideOrShowCommentById")]
        public async Task<IActionResult> HideOrShowCommentById(int commentId)
        {
            var response = await _commentService.HideOrShowCommentById(commentId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("CountInteractByArtworkId")]
        public async Task<IActionResult> CountInteractByArtworkId(int artworkId)
        {
            var response = await _commentService.CountInteractByArtworkId(artworkId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateComment")]
        public async Task<IActionResult> UpdateComment(UpdateCommentModel updateComment, int commentId)
        {
            var response = await _commentService.UpdateComment(updateComment, commentId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }

            return Ok(response);
        }

        [HttpGet("CountCommentByArtworkId")]
        public async Task<IActionResult> CountCommentByArtworkId(int artworkId)
        {
            var response = await _commentService.CountCommentByArtworkId(artworkId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("GetUseLikerOrNotLike")]
        public async Task<IActionResult> GetUseLikerOrNotLike(string userId, int artworkId)
        {
            var response = await _commentService.GetUseLikerOrNotLike(userId,artworkId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("GetCommentForPost")]
        public async Task<IActionResult> GetCommentForPost(int artworkId)
        {
            var response = await _commentService.GetCommentForPost(artworkId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
    }
}
