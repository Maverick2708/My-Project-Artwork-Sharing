using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Repositories
{
	public class OrderDetailRepository : IOrderDetailRepository
	{
		private readonly Artwork_SharingContext _context;

		public OrderDetailRepository(Artwork_SharingContext context)
		{
			_context = context;
		}

		public async Task<OrderDetail> AddOrderDetail(OrderDetail orderDetail)
		{
			_context.OrderDetails.Add(orderDetail);
			await _context.SaveChangesAsync();
			return orderDetail;
		}

		public async Task<IEnumerable<OrderDetail>> GetOrderDetailsByOrderId(int billOrderId)
		{
			return await _context.OrderDetails
				.Where(od => od.BillOrderId == billOrderId)
				.ToListAsync();
		}

		public async Task<bool> RemoveOrderDetail(int orderDetailId)
		{
			var orderDetailToRemove = await _context.OrderDetails.FindAsync(orderDetailId);
			if (orderDetailToRemove == null)
				return false;

			_context.OrderDetails.Remove(orderDetailToRemove);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateOrderDetail(OrderDetail orderDetail)
		{
			var existingOrderDetail = await _context.OrderDetails.FindAsync(orderDetail.OrderDetailId);
			if (existingOrderDetail == null)
				return false;

			// Update order detail properties
			existingOrderDetail.DateOrder = orderDetail.DateOrder;
			existingOrderDetail.PriceOrder = orderDetail.PriceOrder;
			existingOrderDetail.ArtworkPId = orderDetail.ArtworkPId;
			existingOrderDetail.Quanity = orderDetail.Quanity;

			_context.OrderDetails.Update(existingOrderDetail);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
