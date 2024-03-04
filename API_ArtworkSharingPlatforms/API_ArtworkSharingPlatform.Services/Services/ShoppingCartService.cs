using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Repository.Repositories;
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
		private readonly IShoppingCartRepository _shoppingCartRepository;

		public ShoppingCartService(IShoppingCartRepository shoppingCartRepository)
		{

			_shoppingCartRepository = shoppingCartRepository;
		}

		public async Task<ShoppingCart> GetShoppingCartById(int shoppingCartId)
		{
			return await _shoppingCartRepository.GetShoppingCartById(shoppingCartId);
		}

		public async Task<IEnumerable<ShoppingCart>> GetShoppingCartByUser(string userId)
		{
			return await _shoppingCartRepository.GetShoppingCartByUser(userId);
		}
		public async Task<ShoppingCart> AddToShoppingCart(ShoppingCart shoppingCart)
		{
			return await _shoppingCartRepository.AddToShoppingCart(shoppingCart);
		}

		public async Task<bool> RemoveFromShoppingCart(int shoppingCartId)
		{
			return await _shoppingCartRepository.RemoveFromShoppingCart(shoppingCartId);
		}

		public async Task<bool> UpdateShoppingCart(ShoppingCart shoppingCart)
		{
			return await _shoppingCartRepository.UpdateShoppingCart(shoppingCart);
		}

	}
}
