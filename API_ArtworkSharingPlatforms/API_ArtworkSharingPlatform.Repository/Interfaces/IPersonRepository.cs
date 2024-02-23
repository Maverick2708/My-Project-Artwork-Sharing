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
        public Task<AuthenticationResponseModel> SignInAccountAsync(SignInModel signInModel);
        public Task<PersonModel> GetPersonByEmail(string email);
        public Task<ResponeModel> SignUpAccountAsync(SignUpModel signUpModel);
        public Task<ResponeModel> UpdateVerifiedPage (string userId);
        public Task<ResponeModel> UpdateAccount(UpdateProfileModel updateProfileModel,string userId);
        public Task<ResponeModel> ChangePasswordAsync(ChangePasswordModel changePassword);
        public Task<AuthenticationResponseModel> RefreshToken(TokenModel tokenModel);
    }
}
