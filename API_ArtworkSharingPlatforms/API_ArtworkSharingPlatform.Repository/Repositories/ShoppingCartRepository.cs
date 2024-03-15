using API_ArtworkSharingPlatform.Repository.Data;
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

		public async Task<ResponeModel> AddToShoppingCart(CreateShoppingCartModel shoppingCart)
		{
            try
            {
                // Check if the item already exists in the shopping cart
                var existingItem = _context.ShoppingCarts
                    .FirstOrDefault(item => item.ArtworkPId == shoppingCart.ArtworkPId && item.UserId == shoppingCart.UserId);

                if (existingItem != null)
                {
                    // Item already exists, update quantity, and price
                    existingItem.Quanity += shoppingCart.Quanity;
                    existingItem.PriceArtwork += shoppingCart.PriceArtwork; // You may want to adjust this based on your business logic
                }
                else
                {
                    // Item does not exist, add a new item to the shopping cart
                    var newShoppingC = new ShoppingCart
                    {
                        ArtworkPId = shoppingCart.ArtworkPId,
                        UserId = shoppingCart.UserId,
                        Quanity = shoppingCart.Quanity,
                        PriceArtwork = shoppingCart.PriceArtwork,
                        PictureArtwork = shoppingCart.PictureArtwork,
                    };

                    _context.ShoppingCarts.Add(newShoppingC);
                }
                var notification = new Notification
                {
                    UserId = shoppingCart.UserId,
                    ContentNoti = "The item has been added to the cart",
                    DateNoti = DateTime.Now,
                    Status = true,
                    UserIdReceive = shoppingCart.UserId,
                };
				_context.Notifications.Add(notification);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Added shopping cart item successfully", DataObject = existingItem };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while adding the shopping cart item" };
            }
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
