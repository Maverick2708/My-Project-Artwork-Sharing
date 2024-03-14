using API_ArtworkSharingPlatform.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Interfaces
{
    public interface IGenreRepository
    {
        public Task<ResponeModel> GetALLGenre();
        public Task<ResponeModel> UpdateGenre(CreateGenreModel createGenreModel, int genreID);
        public Task<ResponeModel> CreateGenre(CreateGenreModel createGenreModel);
    }
}
