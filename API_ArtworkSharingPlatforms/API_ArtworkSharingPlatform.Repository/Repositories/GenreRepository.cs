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
    public class GenreRepository : IGenreRepository
    {
        private readonly Artwork_SharingContext _context;

        public GenreRepository(Artwork_SharingContext context)
        {
            this._context = context;
        }

        public async Task<ResponeModel> GetALLGenre()
        {
            try
            {
                var existingGenre = await _context.Genres.ToListAsync();

                if (existingGenre.Count>0)
                {
                    
                    return new ResponeModel { Status = "Success", Message = "Found Genre", DataObject = existingGenre };
                }
                else
                {
                    return new ResponeModel { Status = "Success", Message = "Genre not found" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while Get all Genre" };
            }
        }
        public async Task<ResponeModel> UpdateGenre(CreateGenreModel createGenreModel, int genreID)
        {
            try
            {
                var existingGenre = await _context.Genres.FirstOrDefaultAsync(a => a.GenreId == genreID);

                if (existingGenre == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Genre not found" };
                }

                existingGenre = submitGenreChanges(existingGenre, createGenreModel);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Genre updated successfully", DataObject = existingGenre };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updating the Genre" };
            }
        }

        private Genre submitGenreChanges(Genre genre, CreateGenreModel createGenreModel)
        {
            genre.GenreArtwork = createGenreModel.Genre;
            genre.Description = createGenreModel.Description;
            return genre;
        }

        public async Task<ResponeModel> CreateGenre(CreateGenreModel createGenreModel)
        {
            try
            {
                var genre = new Genre
                {
                    GenreArtwork = createGenreModel.Genre,
                    Description = createGenreModel.Description,
                };
                _context.Genres.Add(genre);
                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Added Genre successfully", DataObject = genre };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while adding the Genre" };
            }
        }
    }
}
