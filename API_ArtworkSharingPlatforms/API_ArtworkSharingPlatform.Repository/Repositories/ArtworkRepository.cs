using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;


namespace API_ArtworkSharingPlatform.Repository.Repositories
{
    public class ArtworkRepository : IArtworkRepository
    {
        private readonly Artwork_SharingContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        // private readonly IMapper _mapper;

        public ArtworkRepository(Artwork_SharingContext context, UserManager<Person> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;

        }
        public async Task<ResponeModel> AddArtwork(AddArtwork addArtwork)
        {
            try
            {
                if (addArtwork.UserId == null || addArtwork.PictureArtwork == null || string.IsNullOrWhiteSpace(addArtwork.PictureArtwork))
                {
                    return new ResponeModel { Status = "Error", Message = "UserId and PictureArtwork cannot be null" };
                }
                else
                {
                    var artwork = new Artwork
                    {
                        ContentArtwork = addArtwork.ContentArtwork,
                        Description = addArtwork.Description,
                        PriceArtwork = addArtwork.PriceArtwork,
                        DatePost = DateTime.Now,
                        PictureArtwork = addArtwork.PictureArtwork,
                        UserId = addArtwork.UserId,
                        GenreId = addArtwork.GenreId,
                        Quanity = addArtwork.Quanity,
                        Status = true
                    };
                    _context.Artworks.Add(artwork);
                    await _context.SaveChangesAsync();

                    return new ResponeModel { Status = "Success", Message = "Added artwork successfully", DataObject = artwork };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel{ Status ="Error",Message= "An error occurred while adding the artwork" };
            }
        }
        public async Task<IEnumerable<Artwork>> GetAllArtwork()
        {
            return await _context.Artworks.ToListAsync();
        }
        public async Task<IEnumerable<Artwork>> GetArtworkByArtist(string artist)
        {
            var Artist = await _context.People
                  .Where(c=>c.FullName.Trim().ToLower().IndexOf(artist.Trim().ToLower()) != -1)
                  .Select(u=> u.Id).FirstOrDefaultAsync();

             var artworkByArtis = await _context.Artworks
                       .Where(c =>c.UserId == Artist.ToString()).ToListAsync();

             return artworkByArtis;
            
        }
        public async Task<IEnumerable<Artwork>> GetArtworkByGenre(string genre)
        {
            var Genre = await _context.Genres
                  .Where(c => c.GenreArtwork.Trim().ToLower().IndexOf(genre.Trim().ToLower()) != -1)
                  .Select(u => u.GenreId).FirstOrDefaultAsync();

            var artworkByGenre = await _context.Artworks
                       .Where(c => c.GenreId == Genre).ToListAsync();

            return artworkByGenre;
        }
        public async Task<ResponeModel> UpdateArtwork(UpdateArtworkModel updateArtworkModel, int artworkId)
        {
            try
            {
                var existingArtwork = await _context.Artworks.FirstOrDefaultAsync(a => a.ArtworkPId == artworkId);

                if (existingArtwork == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Artwork not found" };
                }

                existingArtwork = submitArtworkChanges(existingArtwork, updateArtworkModel);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Artwork updated successfully", DataObject = existingArtwork };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updating the artwork" };
            }
        }
        private Artwork submitArtworkChanges(Artwork artwork, UpdateArtworkModel updateArtworkModel)
        {
            artwork.ContentArtwork = updateArtworkModel.ContentArtwork;
            artwork.Description = updateArtworkModel.Description;
            artwork.PriceArtwork = updateArtworkModel.PriceArtwork;
            artwork.PictureArtwork = updateArtworkModel.PictureArtwork;
            artwork.GenreId = updateArtworkModel.GenreId;
            artwork.Quanity = updateArtworkModel.Quanity;
            return artwork;
        }
        public async Task<ResponeModel> GetArtworkById(int artworkid)
        {
            var artwork = await _context.Artworks
                .Where(c=>c.ArtworkPId == artworkid)
                .Join(
                       _context.People,
                       art => art.UserId,
                       person => person.Id,
                       (art, person) => new
                       {
                           artworkid = art.ArtworkPId,
                           contentArtwork = art.ContentArtwork,
                           description = art.Description,
                           priceArtwork = art.PriceArtwork,
                           datePost = art.DatePost,
                           pictureArtwork = art.PictureArtwork,
                           genreId = art.GenreId,
                           userId = art.UserId,
                           quanity = art.Quanity,
                           status = art.Status,

                           Avatar = person.Avatar,
                       }
                ).FirstOrDefaultAsync();
            if (artwork !=null)
            {

                return new ResponeModel { Status = "Success", Message = "Artwork found", DataObject = artwork };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Artwork not found"};
            }
        }

        public async Task<ResponeModel> HideOrShowArtworkById( int artworkId)
        {
            try
            {
                var existingArtwork = await _context.Artworks.FirstOrDefaultAsync(a => a.ArtworkPId == artworkId);

                if (existingArtwork == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Artwork not found" };
                }

                existingArtwork = HideOrShowArtwork(existingArtwork);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Artwork deleted successfully", DataObject = existingArtwork };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while detele artwork" };
            }
        }
        private Artwork HideOrShowArtwork(Artwork artwork)
        {
            if (artwork.Status == true)
            {
                artwork.Status = false;
            }else
            {
                artwork.Status = true;
            }
            return artwork;
        }
        public async Task<ResponeModel> SearchContenArtwork(string content)
        {
            try
            {
                var contentArtwork = await _context.Artworks
                   .Where(c => c.ContentArtwork.Trim().ToLower().IndexOf(content.Trim().ToLower()) != -1)
                   .ToListAsync();

                return new ResponeModel { Status = "Success", Message = "Artwork found", DataObject = contentArtwork };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "Artwork not found" };
            }
        }
        public async Task<ResponeModel> GetArtworkByUserid(string userID)
        {
            try
            {
                var Artwork = await _context.Artworks
                   .Where(c=> c.UserId == userID)
                   .ToListAsync();
                if (Artwork.Count > 0)
                {
                    return new ResponeModel { Status = "Success", Message = "Artwork found", DataObject = Artwork };
                }
                else if (Artwork.Count <=0)
                {
                    return new ResponeModel { Status = "Error", Message = "This user has no posts" };
                }
                else
                {
                    return new ResponeModel { Status = "Error", Message = "Wrong UserID" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "Artwork not found" };
            }
        }
        public async Task<ResponeModel> GetAllPostCreateInMonth(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var countPost = await _context.Artworks
                .Where(c => c.DatePost >= startDate && c.DatePost <= endDate)
                .ToListAsync();

            if (countPost.Count > 0)
            {
                int sumPost = countPost.Count();
                return new ResponeModel { Status = "Success", Message = "Artwork Found ", DataObject = sumPost };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Not Found ", DataObject = 0 };
            }
        }
    }
}
