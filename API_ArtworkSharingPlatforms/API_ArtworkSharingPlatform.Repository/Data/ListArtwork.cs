using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class ListArtwork
    {
        public int ArtworkPId { get; set; }
        public string? ContentArtwork { get; set; }
        public double? PriceArtwork { get; set; }
        public DateTime? DatePost { get; set; }
        public string? PictureArtwork { get; set; }
        public int? GenreId { get; set; }
        public int? UserId { get; set; }
        public int? Quanity { get; set; }
        public bool? Status { get; set; }
    }
}
