using API_ArtworkSharingPlatform.Repository.Data;
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

    }
}
