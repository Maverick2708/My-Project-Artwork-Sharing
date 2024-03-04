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
	public class ShoppingCartRepository : IShoppingCartRepository
	{
		private readonly Artwork_SharingContext _context;

		public ShoppingCartRepository(Artwork_SharingContext context)
		{
			_context = context;
		}


		public async Task<ShoppingCart> GetShoppingCartById(int shoppingCartId)
		{
			return await _context.ShoppingCarts.FindAsync(shoppingCartId);
		}

		public async Task<IEnumerable<ShoppingCart>> GetShoppingCartByUser(string userId)
		{
			return await _context.ShoppingCarts
				.Where(cart => cart.UserId == userId)
				.ToListAsync();
		}

		public async Task<ShoppingCart> AddToShoppingCart(ShoppingCart shoppingCart)
		{
			_context.ShoppingCarts.Add(shoppingCart);
			await _context.SaveChangesAsync();
			return shoppingCart;
		}

		public async Task<bool> RemoveFromShoppingCart(int shoppingCartId)
		{
			var shoppingCart = await _context.ShoppingCarts.FindAsync(shoppingCartId);
			if (shoppingCart == null)
				return false;

			_context.ShoppingCarts.Remove(shoppingCart);
			await _context.SaveChangesAsync();
			return true;
		}

		public async Task<bool> UpdateShoppingCart(ShoppingCart shoppingCart)
		{
			var existingCart = await _context.ShoppingCarts.FindAsync(shoppingCart.ShoppingCartId);
			if (existingCart == null)
				return false;

			// Update cart properties
			existingCart.ArtworkPId = shoppingCart.ArtworkPId;
			existingCart.UserId = shoppingCart.UserId;
			existingCart.Quanity = shoppingCart.Quanity;
			existingCart.PriceArtwork = shoppingCart.PriceArtwork;
			existingCart.PictureArtwork = shoppingCart.PictureArtwork;

			_context.ShoppingCarts.Update(existingCart);
			await _context.SaveChangesAsync();
			return true;
		}
	}
}
