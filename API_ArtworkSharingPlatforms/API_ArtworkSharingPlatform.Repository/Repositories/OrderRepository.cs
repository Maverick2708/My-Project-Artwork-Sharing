using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using MailKit.Search;
using Microsoft.AspNetCore.Mvc;
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
        private readonly Artwork_SharingContext _context2;
        public OrderRepository(Artwork_SharingContext context, Artwork_SharingContext context2)
		{
			this._context = context;
			this._context2 = context2;
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

        public async Task<ResponeModel> CreateOrderV2(string userid, [FromBody] CreateOrderModel createOrderModel)
        {
            try
            {
				var existAccount = await _context.People.Where(c => c.Id == userid).FirstOrDefaultAsync();
                if (existAccount ==null)
                {
                    return new ResponeModel { Status = "Error", Message = "UserId  cannot found" };
                }
                else
                {
                    var order = new Order
                    {
                        UserId = userid,
                        TotalBill = createOrderModel.TotalBill,
                        Status = createOrderModel.Status
                    };
                    _context.Orders.Add(order);
                    await _context.SaveChangesAsync();

                    int billOrderId = order.BillOrderId;
                    foreach (var orderDetailModel in createOrderModel.OrderDetails)
                    {
                        var orderDetail = new OrderDetail
                        {
                            DateOrder = DateTime.Now,
                            BillOrderId = billOrderId,
                            PriceOrder = orderDetailModel.PriceOrder,
                            ArtworkPId = orderDetailModel.ArtworkPId,
                            Quanity = orderDetailModel.Quanity,
                            Address = orderDetailModel.Address,
                            Phone = orderDetailModel.Phone,
                            FullName = orderDetailModel.FullName,
                            Status = true,
                        };

                        // Add each OrderDetail to the DbContext
                        _context.OrderDetails.Add(orderDetail);
                    }
                    var notification = new Notification
                    {
                        UserId = userid,
                        ContentNoti = "You have placed your order successfully",
                        DateNoti = DateTime.Now,
                        Status = true,
                        UserIdReceive = userid,
                    };
                    _context.Notifications.Add(notification);
                    await _context.SaveChangesAsync();
                    var listUserIDRe = await _context.OrderDetails.Where(c => c.BillOrderId == billOrderId)
		                              .Join(
			                              _context.Artworks,
			                              artworkId => artworkId.ArtworkPId,
			                              artworkOrder => artworkOrder.ArtworkPId,
			                              (artworkId, artworkOrder) => new
			                                 {
				                             userID = artworkOrder.UserId,
			                                 }
			                              )
						                  .Distinct()
						                  .ToListAsync();
					if (listUserIDRe.Count > 0)
					{
						foreach (var NotiOrder in listUserIDRe)
						{
							var notiOrder = new Notification
							{
								UserId = userid,
								ContentNoti = $"{existAccount.FullName} - placed your order",
								DateNoti = DateTime.Now,
								Status = true,
								UserIdReceive = NotiOrder.userID,
							};


							_context.Notifications.Add(notiOrder);

						}
						await _context.SaveChangesAsync();
					}
					
                    return new ResponeModel { Status = "Success", Message = "Added order successfully", DataObject = order };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while adding the order" };
            }

        }

        public async Task<ResponeModel> GetOrderByUserId(string userid)
		{
            try
            {
                var orderDetails = await _context.Orders
                    .Where(o => o.UserId == userid && o.Status==true)
                    .Include(o => o.OrderDetails.Where(od=>od.Status==true))
                    .Select(o => new
                    {
                        
                        OrderDetails = o.OrderDetails
                        .Where(od=>od.Status== true)
                        .Select(od => new
                        {
                            o.BillOrderId,
                            o.TotalBill,
                            od.OrderDetailId,
                            od.DateOrder,
                            od.PriceOrder,
                            od.ArtworkPId,
                            od.Quanity,
                            od.Address,
                            od.Phone,
                            od.FullName,
                            od.Status // Include the Status field from OrderDetails
                        }).ToList()
                    })
                    
                    .ToListAsync();

                if (orderDetails == null || !orderDetails.Any())
                {
                    return new ResponeModel { Status = "Error", Message = "No orders found for the given UserID" };
                }

                return new ResponeModel { Status = "Success", Message = "Fetched orders successfully", DataObject = orderDetails };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while fetching orders" };
            }
        }

        public async Task<ResponeModel> GetOrdersByUserIdRe(string userid)
        {
            try
            {
                var orders = await _context.OrderDetails
                    .Where(od => _context.Artworks
                        .Where(a => a.ArtworkPId == od.ArtworkPId && a.UserId == userid)
                        .Any())
                    .Join(
                        _context.Orders.Where(c=>c.Status==true),
                        orderId => orderId.BillOrderId,
                        OrderID => OrderID.BillOrderId,
                        (orderId, OrderID) => new
                        {
                            OrderID = OrderID.BillOrderId,
                            userId = OrderID.UserId,
                            TotalBill = OrderID.TotalBill,
                            OrderDetail = orderId, // Include order details in the result
                            StatusOrderDetail = orderId.Status,
                            StatusOrder = OrderID.Status,
                        }
                    )
                    .Where(od=>od.StatusOrderDetail==true)
                    .GroupBy(result => new
                    {
                        result.OrderID,
                        result.userId,
                        result.TotalBill
                    })
                    .Select(grouped => new
                    {
                        orderID = grouped.Key.OrderID,
                        userId = grouped.Key.userId,
                        totalBill = grouped.Key.TotalBill,
                        orderDetail = grouped.Select(g => g.OrderDetail).ToList()
                    })
                    .ToListAsync();

                if (orders == null || !orders.Any())
                {
                    return new ResponeModel { Status = "Error", Message = "No orders found for the given UserID" };
                }

                return new ResponeModel { Status = "Success", Message = "Fetched orders successfully", DataObject = orders };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while fetching orders" };
            }
        }

        public async Task<ResponeModel> ChangeStatusOrderDetail(int orderDetailID, string UserID)
        {
            try
            {
                var existingOrderDetail = await _context.OrderDetails.FirstOrDefaultAsync(a => a.OrderDetailId == orderDetailID);

                if (existingOrderDetail == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Order not found" };
                }
                await _context.SaveChangesAsync();
                existingOrderDetail = UpdateStatusOrder(existingOrderDetail);

                
                var FullNameByUserId = await _context.People.Where(c=>c.Id== UserID).FirstOrDefaultAsync();
                if (FullNameByUserId !=null)
                {
                    var userID = await _context.Orders.Where(c => c.BillOrderId == existingOrderDetail.BillOrderId).FirstOrDefaultAsync();
                    if (userID != null)
                    {
                        var notification = new Notification
                        {
                            UserId = UserID,
                            ContentNoti = $"{FullNameByUserId.FullName} - Rejected the order because the quantity was out of stock",
                            DateNoti = DateTime.Now,
                            Status = true,
                            UserIdReceive = userID.UserId,
                        };
                        _context.Notifications.Add(notification);
                        await _context.SaveChangesAsync();
                    }
                     
                }

                return new ResponeModel { Status = "Success", Message = "Order updated successfully", DataObject = existingOrderDetail };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updated Order" };
            }
        }
        private OrderDetail UpdateStatusOrder(OrderDetail orderDetail)
        {
            orderDetail.Status = false;
            return orderDetail;
        }
    }
}
