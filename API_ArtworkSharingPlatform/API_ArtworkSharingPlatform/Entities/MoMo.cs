using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Entities
{
    public partial class MoMo
    {
        public string PhoneMoMo { get; set; } = null!;
        public string? NameMoMo { get; set; }
        public bool? Active { get; set; }
    }
}
