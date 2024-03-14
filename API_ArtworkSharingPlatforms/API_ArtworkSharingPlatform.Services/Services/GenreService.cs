using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Services
{
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;

        public GenreService(IGenreRepository genreRepository)
        {
            this._genreRepository = genreRepository;
        }

        public async Task<ResponeModel> GetALLGenre()
        {
            return await _genreRepository.GetALLGenre();
        }
        public async Task<ResponeModel> UpdateGenre(CreateGenreModel createGenreModel, int genreID)
        {
            return await _genreRepository.UpdateGenre(createGenreModel, genreID);
        }
        public async Task<ResponeModel> CreateGenre(CreateGenreModel createGenreModel)
        {
            return await _genreRepository.CreateGenre(createGenreModel);
        }
    }
}
