using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Repositories
{
	public class OrderRepository : IOrderRepository
	{
		private readonly Artwork_SharingContext _context;

		public OrderRepository(Artwork_SharingContext context)
		{
			_context = context;
		}

		public async Task<Order> CreateOrder(Order order)
		{
			_context.Orders.Add(order);
			await _context.SaveChangesAsync();
			return order;
		}

		public async Task<bool> DeleteOrder(int billOrderId)
		{
			var orderToDelete = await _context.Orders.FindAsync(billOrderId);
			if (orderToDelete == null)
				return false;

			_context.Orders.Remove(orderToDelete);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<IEnumerable<Order>> GetArtworkByUser(string userId)
		{
			return await _context.Orders
				.Where(o => o.UserId == userId)
				.ToListAsync();
		}

		public async Task<Order> GetOrderById(int billOrderId)
		{
			return await _context.Orders
				.Include(o => o.OrderDetails)
				.FirstOrDefaultAsync(o => o.BillOrderId == billOrderId);
		}

		public async Task<IEnumerable<Order>> GetOrders()
		{
			return await _context.Orders.ToListAsync();
		}

		public async Task<bool> UpdateOrder(int billOrderId, UpdateOrder updateOrder)
		{
			var existingOrder = await _context.Orders.FindAsync(billOrderId);
			if (existingOrder == null)
				return false;

			// Update order properties
			existingOrder.TotalBill = updateOrder.TotalBill;

			_context.Orders.Update(existingOrder);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> AddOrder(Order order)
		{
			try
			{
				await _context.Orders.AddAsync(order);
				await _context.SaveChangesAsync();
				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		public async Task<ResponeModel> AddOrder(AddOrder addOrder)
		{
			try
			{
				// Convert addOrder to Order entity
				var order = new Order
				{
					// Populate order properties from addOrder
				};

				await _context.Orders.AddAsync(order);
				await _context.SaveChangesAsync();

				return new ResponeModel
				{
					// Populate ResponeModel properties
				};
			}
			catch (Exception ex)
			{
				return new ResponeModel
				{
					// Populate ResponeModel properties with error message
				};
			}
		}
	}
}
