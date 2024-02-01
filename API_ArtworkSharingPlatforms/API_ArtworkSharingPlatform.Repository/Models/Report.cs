using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class Report
    {
        public int ReportId { get; set; }
        public int? ArtworkPId { get; set; }
        public string? UserId { get; set; }
        public string? Description { get; set; }
        public bool? Status { get; set; }

        public virtual Artwork? ArtworkP { get; set; }
        public virtual Person? User { get; set; }
    }
}
