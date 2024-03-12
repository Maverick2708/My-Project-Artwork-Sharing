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
        public Task<ResponeModel> SignUpSuperAdminAccount(SignUpModel signUpModel);
        public Task<ResponeModel> SignUpAdminAccount(SignUpModel signUpModel);
        public Task<ResponeModel> UpdateVerifiedPage (string userId);
        public Task<ResponeModel> UpdateAccount(UpdateProfileModel updateProfileModel,string userId);
        public Task<ResponeModel> ChangePasswordAsync(ChangePasswordModel changePassword);
        public Task<AuthenticationResponseModel> RefreshToken(TokenModel tokenModel);
        public Task<ResponeModel> UpdateAvatar(UpdateAvatarModel avatar, string userId);
        public Task<ResponeModel> UpdateBackGround(UpdateBackGroundModel backGround, string userId);
        public Task<ResponeModel> GetAllAccountBySuperAdmin();
        public Task<ResponeModel> GetAllAccountByAdmin();
        public Task<ResponeModel> GetAllAccountCreateInMonth(int year, int month);

        public Task<ResponeModel> UpdateUserRole(string userId, string selectedRole);
        public Task<ResponeModel> ForgetPasswordAsync(string email);
        public  Task<ResponeModel> ConfirmResetPasswordAsync(string email, string code, string newPassword);

    }
}
