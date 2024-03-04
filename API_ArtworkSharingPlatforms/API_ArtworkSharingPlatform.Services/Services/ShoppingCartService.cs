using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Services
{
	public class ShoppingCartService : IShoppingCartService
	{
		public Task<ShoppingCart> GetShoppingCartById(int shoppingCartId)
		{
			throw new NotImplementedException();
		}

		public Task<IEnumerable<ShoppingCart>> GetShoppingCartByUser(string userId)
		{
			throw new NotImplementedException();
		}
	}
}
