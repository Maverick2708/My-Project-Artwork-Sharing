using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class OrderDetail
    {
        public int OrderDetailId { get; set; }
        public DateTime? DateOrder { get; set; }
        public int? BillOrderId { get; set; }
        public double? PriceOrder { get; set; }
        public int? ArtworkPId { get; set; }
        public int? Quanity { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? FullName { get; set; }
        public bool? Status { get; set; }

        public virtual Artwork? ArtworkP { get; set; }
        public virtual Order? BillOrder { get; set; }
    }
}
