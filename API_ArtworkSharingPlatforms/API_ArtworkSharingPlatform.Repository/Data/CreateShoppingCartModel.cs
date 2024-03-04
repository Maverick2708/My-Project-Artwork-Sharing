using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class CreateShoppingCartModel
    {
        public int? ArtworkPId { get; set; }
        public string? UserId { get; set; }
        public int? Quanity { get; set; }
        public double? PriceArtwork { get; set; }
        public string? PictureArtwork { get; set; }
    }
}
