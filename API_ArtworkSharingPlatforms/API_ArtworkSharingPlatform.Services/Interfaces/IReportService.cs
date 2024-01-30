using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Interfaces
{
    public interface IReportService
    {
        public Task<ResponeModel> CreateReport(AddReportModel addReportModel);
        public Task<IEnumerable<Report>> GetAllReport();
        public Task<ResponeModel> UpdateReport(int reportId);
        public Task<Report> GetReportById(int reportId);
        public Task<Report> GetReportByArtworkId(int artworkId);
        public Task<Report> GetReportByArtistId(int artistId);
        public Task<IEnumerable<Report>> GetReportByArtist(string artist);
    }
}
