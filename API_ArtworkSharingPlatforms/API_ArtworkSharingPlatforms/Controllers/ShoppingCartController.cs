using API_ArtworkSharingPlatform.Repository.Models;
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
		[HttpPost]
		public async Task<IActionResult> AddToShoppingCart(ShoppingCart shoppingCart)
		{
			try
			{
				var addedCart = await _shoppingCartService.AddToShoppingCart(shoppingCart);
				return Ok(addedCart);
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpPut("{shoppingCartId}")]
		public async Task<IActionResult> UpdateShoppingCart(int shoppingCartId, ShoppingCart shoppingCart)
		{
			try
			{
				if (shoppingCartId != shoppingCart.ShoppingCartId)
					return BadRequest("ShoppingCartId mismatch");

				var updated = await _shoppingCartService.UpdateShoppingCart(shoppingCart);
				if (!updated)
					return NotFound();

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

		[HttpDelete("{shoppingCartId}")]
		public async Task<IActionResult> RemoveFromShoppingCart(int shoppingCartId)
		{
			try
			{
				var removed = await _shoppingCartService.RemoveFromShoppingCart(shoppingCartId);
				if (!removed)
					return NotFound();

				return Ok();
			}
			catch (Exception ex)
			{
				return StatusCode(StatusCodes.Status500InternalServerError, $"Internal server error: {ex.Message}");
			}
		}

	}
}
