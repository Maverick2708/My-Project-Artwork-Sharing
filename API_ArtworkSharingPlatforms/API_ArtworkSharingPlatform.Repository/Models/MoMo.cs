﻿using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class MoMo
    {
        public int MomoId { get; set; }
        public string PhoneMoMo { get; set; } = null!;
        public string? NameMoMo { get; set; }
        public bool? Active { get; set; }
    }
}
