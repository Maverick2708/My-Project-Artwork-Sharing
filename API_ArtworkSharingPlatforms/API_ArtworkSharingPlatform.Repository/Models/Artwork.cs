using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class Artwork
    {
        public Artwork()
        {
            Comments = new HashSet<Comment>();
            OrderDetails = new HashSet<OrderDetail>();
            RateStars = new HashSet<RateStar>();
            Reports = new HashSet<Report>();
        }

        public int ArtworkPId { get; set; }
        public string? ContentArtwork { get; set; }
        public double? PriceArtwork { get; set; }
        public DateTime? DatePost { get; set; }
        public string? PictureArtwork { get; set; }
        public int? GenreId { get; set; }
        public int? UserId { get; set; }
        public int? Quanity { get; set; }
        public bool? Status { get; set; }

        public virtual Genre? Genre { get; set; }
        public virtual Person? User { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
        public virtual ICollection<RateStar> RateStars { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
    }
}
