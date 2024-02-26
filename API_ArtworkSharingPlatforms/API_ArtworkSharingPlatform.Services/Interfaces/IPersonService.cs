﻿using API_ArtworkSharingPlatform.Repository.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Services.Interfaces
{
    public interface IPersonService
    {
        public Task<AuthenticationResponseModel> SignInAccountAsync(SignInModel signInModel);
        public Task<PersonModel> GetPersonByEmail(string email);
        public Task<ResponeModel> SignUpAccountAsync(SignUpModel signUpModel);
        public Task<ResponeModel> SignUpSuperAdminAccount(SignUpModel signUpModel);
        public Task<ResponeModel> SignUpAdminAccount(SignUpModel signUpModel);
        public Task<ResponeModel> UpdateVerifiedPage(string userId);
        public Task<ResponeModel> UpdateAccount(UpdateProfileModel updateProfileModel, string userId);
        public Task<ResponeModel> ChangePasswordAsync(ChangePasswordModel changePassword);
        public Task<AuthenticationResponseModel> RefreshToken(TokenModel tokenModel);
        public Task<ResponeModel> UpdateAvatar(UpdateAvatarModel avatar, string userId);
        public Task<ResponeModel> UpdateBackGround(UpdateBackGroundModel backGround, string userId);
    }
}
