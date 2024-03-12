using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Services
{
	public class OrderService : IOrderService
	{
		private readonly IOrderRepository _orderRepository;

		public OrderService(IOrderRepository orderRepository)
		{
			_orderRepository = orderRepository;
		}

		public async Task<bool> AddOrder(Order order) => await _orderRepository.AddOrder(order);

		public async Task<ResponeModel> AddOrder(AddOrder addOrder)
		{
			return await _orderRepository.AddOrder(addOrder: addOrder);
		}

		public async Task<Order> CreateOrder(Order order)
		{
			return await _orderRepository.CreateOrder(order);
		}

		public async Task<bool> DeleteOrder(int billOrderId)
		{
			return await _orderRepository.DeleteOrder(billOrderId);
		}

		public async Task<IEnumerable<Order>> GetArtworkByUser(string userId)
		{
			return await _orderRepository.GetArtworkByUser(userId);
		}

		public async Task<Order> GetOrderById(int billOrderId)
		{
			return await _orderRepository.GetOrderById(billOrderId);
		}

		public async Task<IEnumerable<Order>> GetOrders()
		{
			return await _orderRepository.GetOrders();
		}

		public async Task<bool> UpdateOrder(int billOrderId, UpdateOrder updateOrder)
		{
			return await _orderRepository.UpdateOrder(billOrderId, updateOrder);
		}
        public async Task<ResponeModel> CreateOrderV2(string userid, [FromBody] CreateOrderModel createOrderModel)
        {
			return await _orderRepository.CreateOrderV2(userid, createOrderModel);
		}
        public async Task<ResponeModel> GetOrderByUserId(string userid)
		{
			return await _orderRepository.GetOrderByUserId(userid);
		}
        public async Task<ResponeModel> GetOrdersByUserIdRe(string userid)
		{
			return await _orderRepository.GetOrdersByUserIdRe(userid);
		}
        public async Task<ResponeModel> ChangeStatusOrderDetail(int orderDetailID, string UserID)
		{
			return await _orderRepository.ChangeStatusOrderDetail(orderDetailID, UserID);
		}
    }
}
