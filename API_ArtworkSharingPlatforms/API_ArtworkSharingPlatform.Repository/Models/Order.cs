using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Repository.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BillOrderId { get; set; }
        public string? UserId { get; set; }
        public double? TotalBill { get; set; }
        public bool? Status { get; set; }
        public virtual Person? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
