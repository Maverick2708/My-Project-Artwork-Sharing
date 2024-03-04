using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Migrations;
using API_ArtworkSharingPlatform.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Interfaces
{
	public interface IShoppingCartRepository
	{
		Task<ShoppingCart> GetShoppingCartById(int shoppingCartId);
		Task<IEnumerable<ShoppingCart>> GetShoppingCartByUser(string userId);
		public Task<ResponeModel> AddToShoppingCart(CreateShoppingCartModel shoppingCart);
		Task<bool> UpdateShoppingCart(ShoppingCart shoppingCart);
		Task<bool> RemoveFromShoppingCart(int shoppingCartId);
	}
}
