using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Services
{
    public class ReportService : IReportService
    {
        private readonly IReportRepository _reportRepository;
        public ReportService(IReportRepository reportRepository)
        {
            _reportRepository = reportRepository;
        }
        public async Task<ResponeModel> CreateReport(AddReportModel addReportModel)
        {
            return await _reportRepository.CreateReport(addReportModel);
        }
        public async Task<IEnumerable<Report>> GetAllReport()
        {
            return await _reportRepository.GetAllReport();
        }
        public async Task<ResponeModel> UpdateReport(int reportId)
        {
            return await _reportRepository.UpdateReport(reportId);
        }
        public async Task<Report> GetReportById(int reportId)
        {
            return await _reportRepository.GetReportById(reportId);
        }
        public async Task<IEnumerable<Report>> GetReportByArtist(string artist)
        {
            return await _reportRepository.GetReportByArtist(artist);
        }
        public async Task<Report> GetReportByArtworkId(int artworkId)
        {
            return await _reportRepository.GetReportByArtworkId(artworkId);
        }
        public async Task<Report> GetReportByArtistId(int artistId)
        {
            return await _reportRepository.GetReportByArtistId(artistId);
        }
    }
}
