using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Interfaces
{
    public interface IArtworkRepository
    {
        public Task<IEnumerable<Artwork>> GetArtworkByArtist (string artist);
        public Task<IEnumerable<Artwork>> GetArtworkByGenre(string genre);
        public Task<IEnumerable<Artwork>> GetAllArtwork();
        public Task <Artwork> GetArtworkById (int artworkid);
        public Task<ResponeModel> UpdateArtwork (UpdateArtworkModel updateArtworkModel, int artworkId);
        public Task<ResponeModel> AddArtwork(AddArtwork model);
        public Task<ResponeModel> HideOrShowArtworkById( int artworkId);
    }
}
