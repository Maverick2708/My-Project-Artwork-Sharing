using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Interfaces
{
	public interface IOrderRepository
	{
		public Task<Order> GetOrderById(int BillOrderId);
		public Task<IEnumerable<Order>> GetArtworkByUser(string UserId);
		public Task<IEnumerable<Order>> GetOrders();
		public Task<Order> CreateOrder(Order order);
		public Task<bool> DeleteOrder(int billOrderId);
		public Task<bool> UpdateOrder(int billOrderId, UpdateOrder order);
		Task<bool> AddOrder(Order order);
		Task<ResponeModel> AddOrder(AddOrder addOrder);
		public Task<ResponeModel> CreateOrderV2(string userid, [FromBody] CreateOrderModel createOrderModel);
		public Task<ResponeModel> GetOrderByUserId(string userid);
		public Task<ResponeModel> GetOrdersByUserIdRe(string userid);
        public Task<ResponeModel> ChangeStatusOrderDetail(int orderDetailID,string UserID);
    }
}
