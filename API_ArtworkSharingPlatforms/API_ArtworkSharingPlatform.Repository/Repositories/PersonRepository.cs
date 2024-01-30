using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly Artwork_SharingContext _context;
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public PersonRepository(Artwork_SharingContext context, UserManager<Person> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
        }
        public async Task<ResponeModel> SignUpAccountAsync(SignUpModel signUpModel)
        {
            try
            {
                var exsistAccount = await _userManager.FindByNameAsync(signUpModel.AccountEmail);
                if (exsistAccount == null)
                {
                    var user = new Person
                    {
                        FullName = signUpModel.FullName,
                        Dob = signUpModel.BirthDate,
                        Status = true,
                        UserName = signUpModel.AccountEmail,
                        Email = signUpModel.AccountEmail,
                        PhoneNumber = signUpModel.AccountPhone,
                        Avatar = signUpModel.Avatar,
                    };
                    var result = await _userManager.CreateAsync(user, signUpModel.AccountPassword);
                    string errorMessage = null;
                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync(RoleModel.Audience.ToString()))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(RoleModel.Audience.ToString()));
                        }
                        if (await _roleManager.RoleExistsAsync(RoleModel.Audience.ToString()))
                        {
                            await _userManager.AddToRoleAsync(user, RoleModel.Audience.ToString());
                        }

                        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        return new ResponeModel { Status = "Success", Message = "Create account successfull" };
                    }
                    foreach (var ex in result.Errors)
                    {
                        errorMessage = ex.Description;
                    }
                    return new ResponeModel { Status = "Error", Message = errorMessage };
                }
                return new ResponeModel { Status = "Hihi", Message = "Account already exist" };
            }
            catch (Exception ex)
            {
                // Log or print the exception details
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while checking if the account exists." };
            }
           
        }
    }
}
