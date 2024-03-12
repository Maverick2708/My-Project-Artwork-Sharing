using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Data
{
    public class CreateOrderModel
    {
        public List<CreateOrderDetailModel> OrderDetails { get; set; }
        public double? TotalBill { get; set; }
        public bool? Status { get; set; }

       
    }
}
