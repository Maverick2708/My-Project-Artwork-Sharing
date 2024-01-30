using System;
using System.Collections.Generic;

namespace API_ArtworkSharingPlatform.Entities
{
    public partial class Order
    {
        public Order()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }

        public int BillOrderId { get; set; }
        public int? UserId { get; set; }
        public double? TotalBill { get; set; }

        public virtual Person? User { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
