using API_ArtworkSharingPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ArtworkSharingPlatforms.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : Controller
	{
		private readonly IOrderService _orderservice;

		public OrderController(IOrderService orderservice)
		{
			_orderservice = orderservice;
		}
	}
}
