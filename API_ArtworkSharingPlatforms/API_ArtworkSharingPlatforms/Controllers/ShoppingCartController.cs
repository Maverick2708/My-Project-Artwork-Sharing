using API_ArtworkSharingPlatform.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_ArtworkSharingPlatforms.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ShoppingCartController : Controller
	{
		private readonly IShoppingCartService _shoppingCartService;

		public ShoppingCartController(IShoppingCartService shoppingCartService)
		{
			_shoppingCartService = shoppingCartService;
		}

		[HttpGet("{shoppingCartId}")]
		public async Task<IActionResult> GetShoppingCartById(int shoppingCartId)
		{
			try
			{
				var shoppingCart = await _shoppingCartService.GetShoppingCartById(shoppingCartId);
				if (shoppingCart == null)
					return NotFound();

				return Ok(shoppingCart);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpGet("user/{userId}")]
		public async Task<IActionResult> GetShoppingCartByUser(string userId)
		{
			try
			{
				var shoppingCarts = await _shoppingCartService.GetShoppingCartByUser(userId);
				if (shoppingCarts == null || !shoppingCarts.Any())
					return NotFound();

				return Ok(shoppingCarts);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

	}
}
