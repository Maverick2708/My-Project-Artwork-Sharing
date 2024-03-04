using API_ArtworkSharingPlatform.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Interfaces
{
	public interface IShoppingCartService
	{
		Task<ShoppingCart> GetShoppingCartById(int shoppingCartId);
		Task<IEnumerable<ShoppingCart>> GetShoppingCartByUser(string userId);
	}
}
