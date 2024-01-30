using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Interfaces
{
    public interface IPersonRepository
    {
      //  public Task<AuthenticationResponseModel> SignInAccountAsync();

        public Task<ResponeModel> SignUpAccountAsync(SignUpModel signUpModel);
    }
}
