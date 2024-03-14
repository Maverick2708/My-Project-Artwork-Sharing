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
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            this._genreService = genreService;
        }

        [HttpGet("GetALLGenre")]
        public async Task<IActionResult> GetALLGenre()
        {
            var response = await _genreService.GetALLGenre();
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateGenre")]
        public async Task<IActionResult> UpdateGenre(CreateGenreModel createGenreModel, int genreID)
        {
            var response = await _genreService.UpdateGenre(createGenreModel,genreID);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("CreateGenre")]
        public async Task<IActionResult> CreateGenre(CreateGenreModel createGenreModel)
        {
            var response = await _genreService.CreateGenre(createGenreModel);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
    }
}
