using API_ArtworkSharingPlatform.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Interfaces
{
	public interface IOrderDetailRepository
	{
		public Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int billOrderId);
		public Task<OrderDetail> AddOrderDetail(OrderDetail orderDetail);
		public Task<bool> RemoveOrderDetail(int orderDetailId);
		public Task<bool> UpdateOrderDetail(OrderDetail orderDetail);
	}
}
