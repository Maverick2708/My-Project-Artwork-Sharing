﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class ResponeModel
    {
        public string Status { get; set; }
        public string Message { get; set; } = string.Empty;
        public object DataObject { get; set; } = null;
    }
}
