using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace API_ArtworkSharingPlatform.Repository.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private readonly Artwork_SharingContext _context;
        private readonly IConfiguration configuration;
        private readonly UserManager<Person> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<Person> signInManager;
        private readonly IMapper _mapper;
        public PersonRepository(Artwork_SharingContext context,
                                UserManager<Person> userManager, 
                                RoleManager<IdentityRole> roleManager,
                                IConfiguration Configuration,
                               SignInManager<Person> SignInManager,
                               IMapper mapper)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.signInManager = SignInManager;
            this.configuration = Configuration;
            this._mapper = mapper;
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
                        DateUserRe = DateTime.Now,
                        Gender = signUpModel.Gender,
                        Status = true,
                        Address = signUpModel.Address,
                        UserName = signUpModel.AccountEmail,
                        Email = signUpModel.AccountEmail,
                        PhoneNumber = signUpModel.AccountPhone,
                        Avatar = signUpModel.Avatar,
                        IsVerifiedPage = false,
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

        public async Task<AuthenticationResponseModel> SignInAccountAsync(SignInModel signInModel)
        {
            var result = await signInManager.PasswordSignInAsync(signInModel.AccountEmail, signInModel.AccountPassword, false, false);
            var account = await _userManager.FindByNameAsync(signInModel.AccountEmail);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(signInModel.AccountEmail);
                if (user != null )
                {
                    var authClaims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, signInModel.AccountEmail),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                    var userRole = await _userManager.GetRolesAsync(user);
                    foreach (var role in userRole)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, role.ToString()));
                    }

                    var authenKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));

                    var token = new JwtSecurityToken(
                        issuer: configuration["JWT:ValidIssuer"],
                        audience: configuration["JWT:ValidAudience"],
                        expires: DateTime.Now.AddHours(2),
                        claims: authClaims,
                        signingCredentials: new SigningCredentials(authenKey, SecurityAlgorithms.HmacSha256)
                    );

                    var refreshToken = GenerateRefreshToken();

                    _ = int.TryParse(configuration["JWT:RefreshTokenValidityInDays"], out int refreshTokenValidityInDays);

                    account.RefreshToken = refreshToken;
                    account.RefreshTokenExpiryTime = DateTime.Now.AddDays(refreshTokenValidityInDays);

                    await _userManager.UpdateAsync(account);

                    return new AuthenticationResponseModel
                    {
                        Status = true,
                        Message = "Login successfully!",
                        JwtToken = new JwtSecurityTokenHandler().WriteToken(token),
                        Expired = token.ValidTo,
                        JwtRefreshToken = refreshToken,
                    };
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<PersonModel> GetPersonByEmail(string email)
        {
            var Person = await _userManager.FindByNameAsync(email);
            var result = _mapper.Map<PersonModel>(Person);
            return result;
        }
        private JwtSecurityToken CreateToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"]));
            _ = int.TryParse(configuration["JWT:TokenValidityInMinutes"], out int tokenValidityInMinutes);

            var token = new JwtSecurityToken(
                issuer: configuration["JWT:ValidIssuer"],
                audience: configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddMinutes(tokenValidityInMinutes),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                );

            return token;
        }
        public async Task<ResponeModel> UpdateVerifiedPage(string userId)
        {
            try
            {
                var existingUserId = await _context.People.FirstOrDefaultAsync(a => a.Id == userId && a.IsVerifiedPage == false);

                if (existingUserId == null)
                {
                    return new ResponeModel { Status = "Error", Message = "User not found" };
                }

                existingUserId = HideRequest(existingUserId);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "User updated successfully", DataObject = existingUserId };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while update User" };
            }
        }
        private Person HideRequest(Person person)
        {
            person.IsVerifiedPage = true;
            return person;
        }
    }
}
