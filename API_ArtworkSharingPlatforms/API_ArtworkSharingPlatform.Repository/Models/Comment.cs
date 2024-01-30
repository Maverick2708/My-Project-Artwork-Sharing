using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }
        public string? ContentComment { get; set; }
        public DateTime? DateSub { get; set; }
        public string? UserId { get; set; }
        public int? ArtworkPId { get; set; }
        public bool? IsLikePost { get; set; }
        public bool? Status { get; set; }

        public virtual Artwork? ArtworkP { get; set; }
        public virtual Person? User { get; set; }
    }
}
