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
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        public PersonService(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }
        public async Task<ResponeModel> SignUpAccountAsync(SignUpModel signUpModel)
        {
            return await _personRepository.SignUpAccountAsync(signUpModel);
        }
        public async Task<AuthenticationResponseModel> SignInAccountAsync(SignInModel signInModel)
        {
            var result = await _personRepository.GetPersonByEmail(signInModel.AccountEmail);
            if (result != null)
            {
                return await _personRepository.SignInAccountAsync(signInModel);
            }
            return new AuthenticationResponseModel
            {
                Status = false,
                Message = "Your Account not valid! Please Sign Up to Connect",
                JwtToken = null,
                Expired = null
            };
        }
        public async Task<PersonModel> GetPersonByEmail(string email)
        {
            return await _personRepository.GetPersonByEmail(email);
        }
        public async Task<ResponeModel> UpdateVerifiedPage(string userId)
        {
            return await _personRepository.UpdateVerifiedPage(userId);
        }
        public async Task<ResponeModel> UpdateAccount(UpdateProfileModel updateProfileModel, string userId)
        {
            return await _personRepository.UpdateAccount(updateProfileModel, userId);
        }
        public async Task<ResponeModel> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            return await _personRepository.ChangePasswordAsync(changePassword);
        }
        public async Task<AuthenticationResponseModel> RefreshToken(TokenModel tokenModel)
        {
            return await _personRepository.RefreshToken(tokenModel);
        }
    }
}
