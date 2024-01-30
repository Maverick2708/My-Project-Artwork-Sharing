using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Entities
{
    public partial class Notification
    {
        public int NotificationId { get; set; }
        public int? UserId { get; set; }
        public string? ContentNoti { get; set; }
        public DateTime? DateNoti { get; set; }
        public bool? Status { get; set; }

        public virtual Person? User { get; set; }
    }
}
