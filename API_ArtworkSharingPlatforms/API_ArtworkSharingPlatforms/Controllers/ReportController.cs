using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ArtworkSharingPlatforms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ReportController : Controller
    {
        private readonly IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }
        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport(AddReportModel addReportModel)
        {
            var response = await _reportService.CreateReport(addReportModel);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }
        [HttpGet("GetAllReport")]
        public async Task<IActionResult> GetAllReport()
        {
            var report = await _reportService.GetAllReport();
            return Ok(report);
        }
        [HttpPut("UpdateReport")]
        public async Task<IActionResult> UpdateReport(int reportId)
        {
            var report = await _reportService.GetReportById(reportId);
            var response = await _reportService.UpdateReport(reportId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }

            return Ok(response);
        }
        [HttpGet("GetReportById")]
        public async Task<IActionResult> GetReportById(int reportId)
        {
            var reports = await _reportService.GetReportById(reportId);
            if (reports == null)
            {
                return NotFound();
            }
            return Ok(reports);
        }
        [HttpGet("GetReportByArtist")]
        public async Task<IActionResult> GetReportByArtist(string artist)
        {
            var Artist = await _reportService.GetReportByArtist(artist);
            if (Artist == null)
            {
                return NotFound();
            }
            return Ok(Artist);
        }
        [HttpGet("GetReportByArtworkId")]
        public async Task<IActionResult> GetReportByArtworkId(int artworkId)
        {
            var ArtworkId = await _reportService.GetReportByArtworkId(artworkId);
            if (ArtworkId == null)
            {
                return NotFound();
            }
            return Ok(ArtworkId);
        }
        [HttpGet("GetReportByUserId")]
        public async Task<IActionResult> GetReportByArtistId(string artistId)
        {
            var ArtistId = await _reportService.GetReportByArtistId(artistId);
            if (ArtistId == null)
            {
                return NotFound();
            }
            return Ok(ArtistId);
        }
    }
}
