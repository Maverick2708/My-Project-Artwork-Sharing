using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Entities
{
    public partial class RateStar
    {
        public int RateId { get; set; }
        public int? UserId { get; set; }
        public int? ArtworkPId { get; set; }
        public int? Rate { get; set; }

        public virtual Artwork? ArtworkP { get; set; }
        public virtual Person? User { get; set; }
    }
}
