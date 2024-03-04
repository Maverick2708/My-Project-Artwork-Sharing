using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Interfaces
{
	public interface IOrderService
	{
		public Task<Order> GetOrderById(int BillOrderId);
		public Task<IEnumerable<Order>> GetArtworkByUser(string UserId);
		public Task<IEnumerable<Order>> GetOrders();
		public Task<Order> CreateOrder(Order order);
		public Task<bool> UpdateOrder(Order order);
		public Task<bool> DeleteOrder(int billOrderId);
		public Task<bool> AddOrder(Order order);
		public Task<ResponeModel> AddOrder(AddOrder addOrder);
		public Task<bool> UpdateOrder(int billOrderId, UpdateOrder order);

	}
}
