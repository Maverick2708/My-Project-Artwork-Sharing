using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class AddArtwork
    {
        public string? ContentArtwork { get; set; }
        public string? Description { get; set; }
        public double? PriceArtwork { get; set; }
        public int? GenreId { get; set; }
        public string? UserId { get; set; }
        public string? PictureArtwork { get; set; }
        public int? Quanity { get; set; }
        public bool? Status { get; set; } 

        //public List<int> GenreId { get; set; }

        //public List<int> UserId { get; set; }
    }
}
