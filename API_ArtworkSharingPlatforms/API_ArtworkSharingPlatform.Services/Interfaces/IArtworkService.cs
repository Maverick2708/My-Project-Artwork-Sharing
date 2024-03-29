﻿using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Interfaces
{
    public interface IArtworkService
    {
        public Task<IEnumerable<Artwork>> GetArtworkByArtist(string artist);
        public Task<IEnumerable<Artwork>> GetArtworkByGenre(string genre);
        public Task<ResponeModel> UpdateArtwork(UpdateArtworkModel updateArtworkModel, int artworkId);
        public Task<ResponeModel> GetArtworkById(int artworkid);
        public Task<IEnumerable<Artwork>> GetAllArtwork();
        public Task<ResponeModel> AddArtwork(AddArtwork model);
        public Task<ResponeModel> HideOrShowArtworkById(int artworkId);
        public Task<ResponeModel> SearchContenArtwork(string content);
        public Task<ResponeModel> GetArtworkByUserid(string userID);
        public Task<ResponeModel> GetAllPostCreateInMonth(int year, int month);
        public Task<ResponeModel> GetArtistByArtworkId(int artworkid);
    }
}
