using API_ArtworkSharingPlatform.Repository.Data;
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
    public class ArtworkService : IArtworkService
    { 
        private readonly IArtworkRepository _artworkRepository;
        
        public ArtworkService(IArtworkRepository artworkRepository)
        {

            _artworkRepository = artworkRepository;

        }
        public async Task<ResponeModel> AddArtwork(AddArtwork addArtwork)
        {
            return await _artworkRepository.AddArtwork(addArtwork);
        }
        public async Task<IEnumerable<Artwork>> GetArtworkByArtist(string artist)
        {
            return  await _artworkRepository.GetArtworkByArtist(artist);
        }
        public async Task<IEnumerable<Artwork>> GetArtworkByGenre(string genre)
        {
            return await _artworkRepository.GetArtworkByGenre(genre);
        }
        public async Task<ResponeModel> UpdateArtwork(UpdateArtworkModel updateArtworkModel, int artworkId)
        {
            return await _artworkRepository.UpdateArtwork(updateArtworkModel, artworkId);
        }
        public async Task<ResponeModel> GetArtworkById(int artworkid)
        {
            return await _artworkRepository.GetArtworkById(artworkid);
        }
        public async Task<IEnumerable<Artwork>> GetAllArtwork()
        {
            return await _artworkRepository.GetAllArtwork();
        }
        public async Task<ResponeModel> HideOrShowArtworkById(int artworkId)
        {
            return await _artworkRepository.HideOrShowArtworkById(artworkId);
        }
        public async Task<ResponeModel> SearchContenArtwork(string content)
        {
            return await _artworkRepository.SearchContenArtwork(content);
        }
        public async Task<ResponeModel> GetArtworkByUserid(string userID)
        {
            return await _artworkRepository.GetArtworkByUserid(userID);
        }
        public async Task<ResponeModel> GetAllPostCreateInMonth(int year, int month)
        {
            return await _artworkRepository.GetAllPostCreateInMonth(year, month);
        }
    }
}
