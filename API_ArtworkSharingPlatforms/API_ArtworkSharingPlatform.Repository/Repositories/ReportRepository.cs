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
    public class ReportRepository : IReportRepository
    {
        private readonly Artwork_SharingContext _context;
        public ReportRepository(Artwork_SharingContext context)
        {
            _context = context;
        }
        public async Task<ResponeModel> CreateReport(AddReportModel addReportModel)
        {
            try
            {
                var report = new Report
                {
                    ArtworkPId = addReportModel.ArtworkPId,
                    UserId = addReportModel.UserId,
                    Description = addReportModel.Description,
                    Status = true
                };
                _context.Reports.Add(report);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Added report successfully", DataObject = report };

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while adding the report" };
            }
        }
        public async Task<IEnumerable<Report>> GetAllReport()
        {
            return await _context.Reports.ToListAsync();
        }
        public async Task<ResponeModel> UpdateReport(int reportId)
        {
            try
            {
                var existingReport = await _context.Reports.FirstOrDefaultAsync(a => a.ReportId == reportId);

                if (existingReport == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Artwork not found" };
                }

                existingReport = UpdateStatusReport(existingReport);
               // _context.Reports.Remove(existingReport);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Report updated successfully", DataObject = existingReport };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updated artwork" };
            }
        }
        private Report UpdateStatusReport(Report report)
        {
            if (report.Status == true)
            {
                report.Status = false;
            }
            else
            {
                report.Status = true;
            }
            return report;
        }
        public async Task<Report> GetReportById(int reportId)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(c=> c.ReportId== reportId);
            return report;
        }
        public async Task<IEnumerable<Report>> GetReportByArtist(string artist)
        {
            var Artist = await _context.People
                 .Where(c => c.FullName.Trim().ToLower().IndexOf(artist.Trim().ToLower()) != -1)
                 .Select(u => u.Id).FirstOrDefaultAsync();
            var reportByArtis = await _context.Reports
                       .Where(c => c.UserId == Artist.ToString()).ToListAsync();
            return reportByArtis;
        }
        public async Task<Report> GetReportByArtworkId(int artworkId)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(c => c.ArtworkPId == artworkId);
            return report;
        }
        public async Task<Report> GetReportByArtistId(string artistId)
        {
            var report = await _context.Reports
                .FirstOrDefaultAsync(c => c.UserId == artistId.ToString());
            return report;
        }
    }
}
