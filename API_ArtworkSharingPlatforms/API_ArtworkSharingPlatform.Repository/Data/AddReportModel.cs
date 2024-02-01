using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class AddReportModel
    {
        public int ReportId { get; set; }
        public int? ArtworkPId { get; set; }
        public int? UserId { get; set; }
        public string? Description { get; set; }
    }
}
