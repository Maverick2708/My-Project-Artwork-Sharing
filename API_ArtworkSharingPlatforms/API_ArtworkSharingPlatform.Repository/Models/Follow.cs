using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class Follow
    {
        public int FollowId { get; set; }
        public int? UserId { get; set; }
        public int? UserIdFollow { get; set; }
        public DateTime? DateFollow { get; set; }
        public bool? Status { get; set; }

        public virtual Person? User { get; set; }
    }
}
