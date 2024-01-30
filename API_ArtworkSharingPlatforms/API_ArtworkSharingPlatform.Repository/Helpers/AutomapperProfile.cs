using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Helpers
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile() 
        {
          CreateMap<AddArtwork,Artwork>().ReverseMap();
          
        }
    }
}
