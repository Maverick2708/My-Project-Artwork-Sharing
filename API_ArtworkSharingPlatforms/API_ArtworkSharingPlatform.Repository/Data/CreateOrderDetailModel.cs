using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class CreateOrderDetailModel
    {

        public double? PriceOrder { get; set; }
        public int? ArtworkPId { get; set; }
        public int? Quanity { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
    }
}
