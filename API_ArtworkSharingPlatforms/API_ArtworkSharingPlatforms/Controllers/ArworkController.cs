﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace API_ArtworkSharingPlatforms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ArworkController : Controller
    {
        private readonly IArtworkService _artworkservice;

        public ArworkController(IArtworkService artworkService)
        {
            _artworkservice = artworkService;
        }
        [HttpPost("AddArtwork")]
        public async Task<IActionResult> AddArtwork(AddArtwork addArtwork)
        {
            var response = await _artworkservice.AddArtwork(addArtwork);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
        [HttpGet("GetArtworkByArtist")]
        public async Task<IActionResult> GetArtworkByArtist(string artist)
        {
            var Artist = await _artworkservice.GetArtworkByArtist(artist);
            if (Artist == null)
            {
                return NotFound();
            }
            return Ok(Artist);
        }
        [HttpGet("GetArtworkByGenre")]
        public async Task<IActionResult> GetArtworkByGenre(string genre)
        {
            var Genre = await _artworkservice.GetArtworkByGenre(genre);
            if (Genre == null)
            {
                return NotFound();
            }
            return Ok(Genre);
        }
        [HttpPut("UpdateArtwork")]
        public async Task<IActionResult> UpdateArtwork(UpdateArtworkModel updateArtworkModel, int artworkId)
        {
            var artwork = await _artworkservice.GetArtworkById(artworkId);
            var response = await _artworkservice.UpdateArtwork(updateArtworkModel, artworkId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }

            return Ok(response);
        }
        [HttpGet("GetArtworkById")]
        public async Task<IActionResult> GetArtworkById (int artworkid)
        {
            var artworks = await _artworkservice.GetArtworkById(artworkid);
            if (artworks == null)
            {
                return NotFound();
            }
            return Ok(artworks);
        }
        [HttpGet("GetAllArtwork")]
        public async Task<IActionResult> GetAllArtwork()
        {
            var artwork = await _artworkservice.GetAllArtwork();
            return Ok(artwork);
        }
        [HttpPost("HideOrShowArtisr")]
        public async Task<IActionResult> HideOrShowArtworkById(int artworkId)
        {
            var artwork = await _artworkservice.GetArtworkById(artworkId);
            var response = await _artworkservice.HideOrShowArtworkById( artworkId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }

            return Ok(response);
        }

        [HttpGet("SearchContenArtwork")]
        public async Task<IActionResult> SearchContenArtwork(string content)
        {
            var response = await _artworkservice.SearchContenArtwork(content);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }

            return Ok(response);
        }

        [HttpGet("GetArtworkByUserid")]
        public async Task<IActionResult> GetArtworkByUserid(string userID)
        {
            var response = await _artworkservice.GetArtworkByUserid(userID);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }

            return Ok(response);
        }

        [HttpGet("GetAllPostCreateInMonth")]
        public async Task<IActionResult> GetAllPostCreateInMonth(int year, int month)
        {
            var response = await _artworkservice.GetAllPostCreateInMonth(year,month);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }

            return Ok(response);
        }

        [HttpGet("GetArtistByArtworkId")]
        public async Task<IActionResult> GetArtistByArtworkId(int artworkid)
        {
            var response = await _artworkservice.GetArtistByArtworkId(artworkid);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }

            return Ok(response);
        }
    }
}
