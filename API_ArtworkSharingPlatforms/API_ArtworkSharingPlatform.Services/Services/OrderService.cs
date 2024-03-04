using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Services.Interfaces;
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

		public Task<bool> AddOrder(Order order)
		{
			throw new NotImplementedException();
		}

		public Task<ResponeModel> AddOrder(AddOrder addOrder)
		{
			throw new NotImplementedException();
		}

		public async Task<Order> CreateOrder(Order order)
		{
			return await _orderRepository.CreateOrder(order);
		}

		public async Task<bool> DeleteOrder(int billOrderId)
		{
			return await (_orderRepository.DeleteOrder(billOrderId));
		}

		public async Task<IEnumerable<Order>> GetArtworkByUser(string UserId)
		{
			return await _orderRepository.GetArtworkByUser(UserId);
		}

		public async Task<Order> GetOrderById(int BillOrderId)
		{
			return await _orderRepository.GetOrderById(BillOrderId);
		}

		public async Task<IEnumerable<Order>> GetOrders()
		{
			return await _orderRepository.GetOrders();
		}

		public async Task<bool> UpdateOrder(int BillOrderId, UpdateOrder updateOrder)
		{
			return await _orderRepository.UpdateOrder(BillOrderId, updateOrder);
		}

		public Task<bool> UpdateOrder(Order order)
		{
			throw new NotImplementedException();
		}
	}
}
