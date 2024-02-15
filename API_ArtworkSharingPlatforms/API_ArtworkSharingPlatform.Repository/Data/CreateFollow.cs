using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class CreateFollow
    {
        public string? UserId { get; set; }
        public string? UserIdFollow { get; set; }
    }
}
