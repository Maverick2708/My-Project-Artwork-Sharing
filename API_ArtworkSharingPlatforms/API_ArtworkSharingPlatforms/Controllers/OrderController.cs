using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ArtworkSharingPlatforms.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : Controller
	{
		private readonly IOrderService _orderService;

		public OrderController(IOrderService orderService)
		{
			_orderService = orderService;
		}
		// GET: api/order/{billOrderId}
		[HttpGet("{billOrderId}")]
		public async Task<IActionResult> GetOrderById(int billOrderId)
		{
			try
			{
				var order = await _orderService.GetOrderById(billOrderId);
				if (order == null)
					return NotFound();

				return Ok(order);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		// POST: api/order
		[HttpPost]
		public async Task<IActionResult> CreateOrder(AddOrder addOrder)
		{
			try
			{
				var response = await _orderService.AddOrder(addOrder);
				if (response.Status == "Error")
				{
					return Conflict(response);
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		// PUT: api/order/{billOrderId}
		[HttpPut("{billOrderId}")]
		public async Task<IActionResult> UpdateOrder(int billOrderId, UpdateOrder updateOrder)
		{
			try
			{
				var response = await _orderService.UpdateOrder(billOrderId, updateOrder);
				if (!response)
				{
					return NotFound(response);
				}
				return Ok(response);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		// GET: api/order
		[HttpGet]
		public async Task<IActionResult> GetOrders()
		{
			try
			{
				var orders = await _orderService.GetOrders();
				return Ok(orders);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		// DELETE: api/order/{billOrderId}
		[HttpDelete("{billOrderId}")]
		public async Task<IActionResult> DeleteOrder(int billOrderId)
		{
			try
			{
				var isDeleted = await _orderService.DeleteOrder(billOrderId);
				if (!isDeleted)
				{
					return NotFound();
				}
				return Ok("Order deleted successfully");
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}
	}
}
