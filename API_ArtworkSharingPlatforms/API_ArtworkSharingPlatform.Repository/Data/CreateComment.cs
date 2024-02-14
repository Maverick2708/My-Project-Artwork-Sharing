using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class CreateComment
    {
        public string? ContentComment { get; set; }
        public string? UserId { get; set; }
        public int? ArtworkPId { get; set; }
     }
}
