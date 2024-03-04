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
	}
}
