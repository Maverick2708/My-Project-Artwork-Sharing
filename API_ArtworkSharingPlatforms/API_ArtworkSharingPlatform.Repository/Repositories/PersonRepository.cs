using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using MailKit.Net.Smtp;
using MimeKit;

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
        private readonly IConfiguration _configuration;
        
        public PersonRepository(Artwork_SharingContext context,
                                UserManager<Person> userManager, 
                                RoleManager<IdentityRole> roleManager,
                                IConfiguration Configuration,
                               SignInManager<Person> SignInManager,
                               IMapper mapper,
                               IConfiguration configuration)
        {
            this._context = context;
            this._userManager = userManager;
            this._roleManager = roleManager;
            this.signInManager = SignInManager;
            this.configuration = Configuration;
            this._mapper = mapper;
            _configuration = configuration;
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
                        Avatar = "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",
                        BackgroundImg = "/9j/4AAQSkZJRgABAQEBLAEsAAD/4QBWRXhpZgAATU0AKgAAAAgABAEaAAUAAAABAAAAPgEbAAUAAAABAAAARgEoAAMAAAABAAIAAAITAAMAAAABAAEAAAAAAAAAAAEsAAAAAQAAASwAAAAB/+0ALFBob3Rvc2hvcCAzLjAAOEJJTQQEAAAAAAAPHAFaAAMbJUccAQAAAgAEAP/hDIFodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0n77u/JyBpZD0nVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkJz8+Cjx4OnhtcG1ldGEgeG1sbnM6eD0nYWRvYmU6bnM6bWV0YS8nIHg6eG1wdGs9J0ltYWdlOjpFeGlmVG9vbCAxMC4xMCc+CjxyZGY6UkRGIHhtbG5zOnJkZj0naHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyc+CgogPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9JycKICB4bWxuczp0aWZmPSdodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyc+CiAgPHRpZmY6UmVzb2x1dGlvblVuaXQ+MjwvdGlmZjpSZXNvbHV0aW9uVW5pdD4KICA8dGlmZjpYUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpYUmVzb2x1dGlvbj4KICA8dGlmZjpZUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpZUmVzb2x1dGlvbj4KIDwvcmRmOkRlc2NyaXB0aW9uPgoKIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PScnCiAgeG1sbnM6eG1wTU09J2h0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8nPgogIDx4bXBNTTpEb2N1bWVudElEPmFkb2JlOmRvY2lkOnN0b2NrOmU5NDVkMzA4LTk4YjItNGM3Mi1iZDhhLWUxYzg5ZDE2N2JjMzwveG1wTU06RG9jdW1lbnRJRD4KICA8eG1wTU06SW5zdGFuY2VJRD54bXAuaWlkOmMyNjgxNTE4LTZmMmEtNGExYi05ODUwLWJiMjM0NWE1NjIyZDwveG1wTU06SW5zdGFuY2VJRD4KIDwvcmRmOkRlc2NyaXB0aW9uPgo8L3JkZjpSREY+CjwveDp4bXBtZXRhPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSd3Jz8+/9sAQwAFAwQEBAMFBAQEBQUFBgcMCAcHBwcPCwsJDBEPEhIRDxERExYcFxMUGhURERghGBodHR8fHxMXIiQiHiQcHh8e/9sAQwEFBQUHBgcOCAgOHhQRFB4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4e/8AAEQgBaAJAAwEiAAIRAQMRAf/EABoAAQEBAQEBAQAAAAAAAAAAAAIDAQAEBQj/xAAuEAACAgIBBAEDAwUBAQEBAAAAAQIRITFBElFhcYEDkaEiscETMtHh8PFSQmL/xAAWAQEBAQAA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",
                        IsVerifiedPage = false,
                        IsConfirm = false,
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
                return new AuthenticationResponseModel
                {
                    Status = false,
                    Message = "Invalid login attempt. Please check your email and password."
                };
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

        public async Task<ResponeModel> UpdateAccount(UpdateProfileModel updateProfileModel, string userId)
        {
            try
            {
                var existingAccount = await _context.People.FirstOrDefaultAsync(a => a.Id == userId);

                if (existingAccount == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Account not found" };
                }

                existingAccount = submitAccountChanges(existingAccount, updateProfileModel);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Account profile updated successfully", DataObject = existingAccount };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updating the account profile" };
            }
        }
        private Person submitAccountChanges(Person account, UpdateProfileModel updateProfileModel)
        {
            account.FullName = updateProfileModel.FullName;
            account.Dob = updateProfileModel.BirthDate;
            account.PhoneNumber = updateProfileModel.PhoneNumber;
            account.Address = updateProfileModel.Address;
            account.Gender = updateProfileModel.Gender;
            return account;
        }

        public async Task<ResponeModel> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            var account = await _userManager.FindByEmailAsync(changePassword.Email);
            if (account == null)
            {
                return new ResponeModel
                {
                    Status = "Error",
                    Message = "Can not find your account!"
                };
            }
            var result = await _userManager.ChangePasswordAsync(account, changePassword.CurrentPassword, changePassword.NewPassword);
            if (!result.Succeeded)
            {
                return new ResponeModel
                {
                    Status = "Error",
                    Message = "Cannot change pass"
                };
            }

            return new ResponeModel
            {
                Status = "Success",
                Message = "Change password successfully!"
            };
        }
        public async Task<AuthenticationResponseModel> RefreshToken(TokenModel tokenModel)
        {
            if (tokenModel is null)
            {
                return new AuthenticationResponseModel
                {
                    Status = false,
                    Message = "Request not valid!"
                };
            }

            string? accessToken = tokenModel.AccessToken;
            string? refreshToken = tokenModel.RefreshToken;

            var principal = GetPrincipalFromExpiredToken(accessToken);
            if (principal == null)
            {
                return new AuthenticationResponseModel
                {
                    Status = false,
                    Message = "Invalid access token or refresh token!"
                };
            }

            string username = principal.Identity.Name;

            var user = await _userManager.FindByNameAsync(username);

            if (user == null || user.RefreshToken != refreshToken || user.RefreshTokenExpiryTime <= DateTime.Now)
            {
                return new AuthenticationResponseModel
                {
                    Status = false,
                    Message = "Invalid access token or refresh token!"
                };
            }

            var newAccessToken = CreateToken(principal.Claims.ToList());
            var newRefreshToken = GenerateRefreshToken();

            user.RefreshToken = newRefreshToken;
            await _userManager.UpdateAsync(user);

            return new AuthenticationResponseModel
            {
                Status = true,
                Message = "Refresh Token successfully!",
                JwtToken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                Expired = newAccessToken.ValidTo,
                JwtRefreshToken = newRefreshToken
            };
        }
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SecretKey"])),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token unavailabel!");
            return principal;
        }

        public async Task<ResponeModel> SignUpSuperAdminAccount(SignUpModel signUpModel)
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
                        Avatar = "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",
                        BackgroundImg = "/9j/4AAQSkZJRgABAQEBLAEsAAD/4QBWRXhpZgAATU0AKgAAAAgABAEaAAUAAAABAAAAPgEbAAUAAAABAAAARgEoAAMAAAABAAIAAAITAAMAAAABAAEAAAAAAAAAAAEsAAAAAQAAASwAAAAB/+0ALFBob3Rvc2hvcCAzLjAAOEJJTQQEAAAAAAAPHAFaAAMbJUccAQAAAgAEAP/hDIFodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0n77u/JyBpZD0nVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkJz8+Cjx4OnhtcG1ldGEgeG1sbnM6eD0nYWRvYmU6bnM6bWV0YS8nIHg6eG1wdGs9J0ltYWdlOjpFeGlmVG9vbCAxMC4xMCc+CjxyZGY6UkRGIHhtbG5zOnJkZj0naHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyc+CgogPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9JycKICB4bWxuczp0aWZmPSdodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyc+CiAgPHRpZmY6UmVzb2x1dGlvblVuaXQ+MjwvdGlmZjpSZXNvbHV0aW9uVW5pdD4KICA8dGlmZjpYUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpYUmVzb2x1dGlvbj4KICA8dGlmZjpZUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpZUmVzb2x1dGlvbj4KIDwvcmRmOkRlc2NyaXB0aW9uPgoKIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PScnCiAgeG1sbnM6eG1wTU09J2h0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8nPgogIDx4bXBNTTpEb2N1bWVudElEPmFkb2JlOmRvY2lkOnN0b2NrOmU5NDVkMzA4LTk4YjItNGM3Mi1iZDhhLWUxYzg5ZDE2N2JjMzwveG1wTU06RG9jdW1lbnRJRD4KICA8eG1wTU06SW5zdGFuY2VJRD54bXAuaWlkOmMyNjgxNTE4LTZmMmEtNGExYi05ODUwLWJiMjM0NWE1NjIyZDwveG1wTU06SW5zdGFuY2VJRD4KIDwvcmRmOkRlc2NyaXB0aW9uPgo8L3JkZjpSREY+CjwveDp4bXBtZXRhPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSd3Jz8+/9sAQwAFAwQEBAMFBAQEBQUFBgcMCAcHBwcPCwsJDBEPEhIRDxERExYcFxMUGhURERghGBodHR8fHxMXIiQiHiQcHh8e/9sAQwEFBQUHBgcOCAgOHhQRFB4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4e/8AAEQgBaAJAAwEiAAIRAQMRAf/EABoAAQEBAQEBAQAAAAAAAAAAAAIDAQAEBQj/xAAuEAACAgIBBAEDAwUBAQEBAAAAAQIRITFBElFhcYEDkaEiscETMtHh8PFSQmL/xAAWAQEBAQAAAAAAAAAAAAAAAAAAAQL/xAAWEQEBAQAAAAAAAAAAAAAAAAAAEQH/2gAMAwEAAhEDEQA/APyjFYGkjoocUbZYoqxVwbQ0vAB6RKIlHkaXNATUc6Eo5yNISj+5YJqPcSjopWtCjEAKIlHBRRFGNrRRNQzoSiikY5GodwVJQGoLsiqjkcY67liJR+mmsDjCuPsVUcaspGGtCCMYeCkYZqisYZtIcIXwWCUfprhL/BRQzhFYw7IrGH2LEqC+nbunRRfSrgvH6enVFF9NVoFeeH01/kovprdcnoj9JqOii+m2sLZYjzf0l4zvA/p/S1eV2Z6l9PTr4HH6WKq0IPKvpZ0KP0lf/wCcnr/pOqS2KP06S8bwIPJ/QynwvyJfRxW7PWvpLPYcfpPHjlosHjX0c60d/Sykkj3f06jqvg3+lbrQg8a+krqg/wBHGqPc/pJZetmr6TTWRCvB/STf9q+Tv6Wf7aPe/pXabpoxfTy35EHz19Hxl8mf0t0vwfQ/ptr8ZC/ptv8AAg+fL6Su6phl9FJU4uz6Dg83GgS+naWBB89/S7JeQv6NbR739LNvIX9JJXWiQfOl9PaS9Al9LLaSp6Z75/SXCDKFYr7iD57+n2VsnP6fJ739NarIJfTvhURXhl9N07JP6eD3ShvHJOf03QK8T+nYH9M9soZusE5/TVvZIV4nBUCX062keuUOwZwxkkV45QQHDGUeuUEBxp5Qg8rgr0BxysHqcG8gcLbSRCvM41kDj4PS4ZA4ha88o84McfBeUG97C1XfAEHFaoLj4LuLMcSCHT9wuOi7iFxEEemg9JdxC0QQcQtFmgyQEWgyRVoDQDiikUGCKRXkDoq+44rwdFFEu5RiXgSQooSKMUciUcWKtYElS8gHp8f7FGOxxiJR7hKCj4Go3jYlF9ikVnF2WARj4GoiUfFjjGwDGF+yih+EJRzyUUbZQIwVfJSMV2FCPNFYx7YCJxgqKqDp8LsOMa5KRjjXsqBGGCsIVjGBxjiu5VRd0lRROEHxyUhB4VLP4K/Tj8UOMXaVYAnGCf8A6VjBPNK+GUUbWdaKKDxS0UT/AKaVfwiihjNY+xWEFVu+xSMGlclgqIR+muz0UX0e2a7F4wzT5VjSa3XwWCK+krtr/qE/pp/q5PQoOurXYUYPbq62IPL/AE7eFQ39POU3FnpcX83tGqD6brbpFHll9Ppi3jts5fSar9Ly9nra0+m7XJqjW7YHkX0aX9trYX9Km6X5PZKGWm/1XnGDH9POG6XcDxy+latq3+4JfSVXR7lF537aD0ZeM8USDxf0227t474A/pyytfwe1wbVu9AcL/VWhB4X9NXh+/IJQ4q3+x7pQbxnPAJ/TXj4JCvBP6dPedgnBvKw+bR7ZwktonKD7fBIPDKPPGwTgtcnsnFungm4um1x4CvE4PSJShWafo9s4Uuy0Sn9NJcLzRIPG4JZz5Jyg9Kz1yiqprzZKUa1og8koreycor8HrlFpWCUO4HlceCTjXg9Uk3eATiSK80o4JuOex6XHwybjxRFedxu+wHHuj0uJNpgeeUTOnwyzjaC99skEGg9JZxYWsBUWjGi0kgUgJOOQtclZLAGmBFrAZIs1wCS7EEZLYJLBWSBJYINiVSwCK7FIgKK8lIhiUjs0NSHFUZEovQHJCS9mpZQlHOAjP2Gln+DYoUV8lHJfgcUjooaX3A6KHFJPP3OiuSkYlHVRRLgxK2n2KKOiprYL/8AmvBWKXwGEfNZKxV8UEbFZ0ikI29HRVuisFiv+ZYOjHWLV8lYxzq/g6EXd9iqi8WUd9NJqu5WMe+OKoyMW2uV+5WEaV4XyMG/TWd34Kxhbz9zoRdJUslYJYctVuyoxQtq0vDKqLcnfFN0aorPF+NlIRekq9mhij+nHOsD6FhJV5o2MXmLWcUVStVarlACKu6T7Cin0p6yOKae4pvDFGNSvCz7AEYtO1eKyPpaVO1Y4Ryr2zYxdWk3yWCSjb1zQumSpt+rRWMaba0teDul138CIjVxfRVmdKcX+l44LdLtr3hbOkl1ZXPCEEJL9KbXFgcd48bPR0trterC44fVzoivN006rPN6D9SKrxWE2ehxppt49Akktq1rWgPO408NW0TnFU9J8npkstOqJuPLSQHklGuN9icktvPij1SXjfiyc6Uq1fkg8soNXrfKJTir1/J6ZK6VX3yTcWsNYJB5WlTrZKUEnpVZ6ZRy6X2JyjdcfyRXlkqWUic4U+KfJ6JRpvGiU0+QPPJPNk2n2+KPRKNPXBKaVXZkeeUe6JyXdNHocc80Smre/YEZR8aJteMey7indE+ni8kV52uQu/8AJdrvgnX/AFEVFpgayWknQGgJSWw0UadaC1wQTaC0Ua8Bku6Ak1aCykqbA0FSf47gZVrgnLAEpInJFZYsnMyNiVj2Jw0isQHHgpFcAisFEaDiOOXoKWEOPkJpR0JGJZEvwAojjnkK8MS7rPkocVoaWqBHsii/dAOO9DivlAXkpGq1+C4hxpaKR2l5JxXCWCsFjt4KhxzRWNXdaJx9fBSCzaArFZvPhFFwwQw9FE+DQotp5/2Vj/3sEVeadFIrl0BWOecaLQqnvRGNeCsFca75ZUUgnrzsrGKpt4onFWnfBWDprv37lFYRb9tlFvpoEXwmlW+BpPpp3jnRRWKz3dYe6FHKVyxx3DG7xb80VxbwsZd4A1VVLb7mrL6VJ2vN5MV9VLeh/wD56nvuVGpNK2qSXDwbHOU/Xg7DlWP1LAnbpNeKfYDFe3XUsUdFLfT1eDaaena5o3HZfYDHw1WMu+Ds43n8GtPo48WHTbyq1kAppXz5aMl5vPPgTVqXbkMk09+gJypY329hkt8JursUkqwnQZcdSttK0NVJ2/019mTmstfwVlq6wTlTWqIJu6rnkk1W7TKz/Vbq/miclHS+4EGuqupNdkSksZeS891h+iM2k8MyJyTSf6SM1XdlZt8P/uxKTzn8k1Uppb3fBKSTeclWsttNslNfNASkk+5J1bvRas1X2JSWMZJojKrfYnJbvZaSx5JPEn9iCT8PknPukVkiclzQVKSzdgl4Ky8E2m9k1U5cgZSVVsD2QTlWtBY3WQMAS7oLV9hy9ha2QCWgNZG1gD0FTl2Jy7Vgq+bJyAkycisiUzI2CVIrGiUC0S4KRKRwya0UjoocRx2CJSPfAQlwOvQVnwJANLP+RJY2FdxlCSKRzjGCccVWRpAUVcNDXH+Axb4Q1jf4NYmnHjL9lVneScfz5KQfjfcIpDBSOWr/ACTjhrNWii9YArHzTKRwuCcLawtspBK9JezQtFLu+dFIay7f7Eot1n0W+mlqrsCkeyv4Kw2v4JRrs8KqsrB4WEVFl2bKrMbTWXwRhbbW2/wUTz50y4LQapWikXrNatMkmqy6KrGLT4RRVaStocVj1zyTWWupZq9DTxhewikVhvXSJJpW7reyarssdyize2nvOChW3UcsSf6U6STzSekBf3Vf6ltvFi6sqWeyyAk47vPODrpZt/ydb6Kxb4SwZFvDStpUgjUsu3hPuY5LrtXo56y8X+WZy73+UBkv7m1fZmNrqatGuW813XcDbbxflVgK6Vp7Stk5Ps6wa23a3d5DKTWLt1kig27S6sJYzkDedq1wJ2487z/gnXS64bADdbdJ/clJRylxRSV9WvgnLulTbYE/q4Vc8dyP1U072/HJWW2ksIjO0qWOF5MgT/t9kprS58lJvFPf7kptqDv2iaYlLPnBJ3xi2Wby8fJGTvNfAVOSxtMlOlWXotLslZJk0Snd98Emq33Kz4wTlWcYIJT/AOVgkt0yjX+ycubwBOXaycr4+xWVbSJyS0TWk5ca+AvPI5YA1ggDfcPwOTr/ACB62AHX5C8XkUvBjuyCbBL2OWAysCUtk5FpeiUvAVKX7ck53orIlPRNGxKxJRKwx4GCka0UjTeWThoqvJQ0+w449AXZ8jWQhr2OP7AX/g0lxsBRsawCK9IaWslDXocbvWfQFveRrHYCkd5bwOPyTjjuNU/TLiKxefJRPJKPlFIvuiopFlIfpTp+2Tg8V3KKsVwu4FVXcrF3FJyIq0Vjdqii0dv7FYPVaX3IQpL32Kxt54KLLeH2LQaeLrukQi31VdFYNp3TdouItHv25KRdXlUtVyRjy7vgssVT7VkootLNKt2V+nJSV3j9yEW2nXPJWPCUcd6KLRlbz9rsal4dvXYim0+fHNDj1Urp8gWTz+p67IblpdJFyrBTCy61phCU12b+KGu3VS9k7bq5W6vKNTeVq+U/uCKNxcd2u/byZ1Km8pcPknccdXC7cGqSbqnfktIpfPPYxNp3dLZjf6dBdJOsikaqpKnfkx4Vc2FtNNO/TMdLLtPwQjMJ4fyguVK9ellnPimvjAZJV3S2BknVx5JykurL3tCbW1m9UiU8PHbVaCsnzlN+/wCCMmtN034HLK/U7YG3TjWKIA2lFfqrwTk9pOvJs+/P8g+o+cY8kE5VTuT9EpbST1opJ55vgjKm3/yJpgturePkn9T/AOtUhv1/4Tk2+Aqf1O9kZPw1+xWWqx9iUnfBNAk95JS422Uk8eCb5sgnJ4/uYJPy87wOTJyALtbJydjeeNgad3Vk1oXnWQNb7je6wTladEBe/AWJ19gvwwA/LC8ibwF61lkAlYHvgcreQSeQA9k59ijJy2FSeSU92VlonImjoWVhvJKPcrAYKRtlI6ROP2KJlFEOPkERoGmtjWdAVWJPuXENexJ9kCL3gaAaHHtQE6dL5Etvn4AorrRSJOPsceL2XBRaeFkat8/7Jp5uykVw/wAFZVjTqrKQWf8ARKL74HFruvuBaPnTZRNLGSUe1YopHXPrsUVi6x3KwwsMirT36KxeHrPJRb6b+5RS9Xsh9KqW8FIyQF4vlVjKKRaUUm6XjuRVXp4KRec08lRZPhX/AB7LRlr7s86deEOMldXnyXB6IuklVdhRed23x3I9WFbevuNvqtq9bKLRl+ru/InK5PPBGL/Skq8d/gSfZ49ZAtbru7+wuvnklKVST06wapqm750BW49WlS8qjOpKLaS8dycpUnSaXFI68bdrm8gUTqOMmNPFbvhg6ncrpU+Ec5Se61tcgb1XLKXbWjPqP/6zTDdR8+zOtp08+gNk6STDKVLOvYZNOWPWgSllfvQCb4ffZNtdKz3dGdWOc5DKnhq6/cDptXbeERlnDV9qQpSzlOicnlu+M2QGXU7snJt1un+BuVdSS5JSccU8aTRFGWVnRJ3nApt+2TlLN/8AIgE0lw/gD/ueXa0KUs1bJy1kATv/AOrXonNrWkhzd+yct4bJoEr0kTlm8+RPQJ69EBk87QJap/7E35JvF4C4LpZJ38/wOX8Af3Jqg6C9dxO6oMtf4IC3jIJUJvIZeAC/AXoTwu4PJAZVewO9je7BLwDE5fknIrLknIKlL0SkWl+SUyaOgViSgVhuhgoikdk4lI8FFFsaJr9hrsEOPkS7BjpCWKYwOPqxR7phzYkUNZQlwvwFZ2hx3wwHHsOL1eUTW3WBrWi4KLuNNX8k75yOL8FRWOu/yUi7VslH5HYRa+5RdlzojB5Sf5GvnuUXT4KRu6/6yEJK8srF/covF3hfA4tdP6e3Y88XzxRWG6WLAvCtRqrKQljPshGVbdlIy29AXjKmmn8lIyrLeKpnnTqOMpjTrDbwVF+qsd9jctvFbbPPF7tcdxx5alab5ZR6ItpeTYS1WyEZOrSpcii759ZKLJySWc6YrupW8ckVJrDz/Jznzj4egKqT22/JsnTqlaJ9WK1XLN6+e4CUqrt6NTXhMltOsLvtHNrldrbAblFvKz49By7fjVAvCy++DMON088ANze6j47UCb0mzHKm2sAlJ52BvVwlhaDKTV2vJjazjPIHLCS47kGSazlhlJbZjdOq85A3xfyQY95+9gm/1d2dKXLBKWc19iKybVUmu+yU3TtfBspOwSeH9wMnfb4JyduotL4NlKNtv9ibbqnyBlp29EntO6E+eGCbMgyazbA32+cilvjAJP2AZ1eOQSutGydoDdvQUZ6qrsL4YnS33JyWCKMzJO/jsbtBeckBfdMF4oTC+ACzGbLdP7BZAX/zJvQ5UsgkgYEsglocicmFTkSnorLeyU9E0dAtElDgrDWhgpHJSOqJxHDBRRawOIIjQQ0JATSYlyA0JB5yxclDjq9iXkEdijXcCq/Ak/uTXdDT5KKReMfccXVdmTXF4HF16KikK7Z8jT7LZJcFI+6CKRvJSN158El7vuOPe8AVUv1FE3y6rWCMe6/JRfqt2n3KLxbvfsadPGmQjK1iqKRk6rwUXi1p2OMspPJBPG7HF5V4YF4y/U28eBwar18kVLkSqt86KLKTqm0vQrxTbJQfKbwK8c+Qi0Wq7+xXeW7I9X6dPsJTeLvIFepevArt8Lgj1Sq8eDk1SSZaLdeaT/Jzk1WFuvRJvN2sZo68OrsUV6s32vRnVjK/kn1K1i6MlJ3Vr7iilvbeO4W/OASdXeGzJStPaXIDdO39guVvKS+AdVNc/wAmWs2/uQa5Y278hcuMGK34A2r38AbJgbrfvZjleHQHva8EV0nwq8UGTxV6Mk89ryCTalv7cAY20njPkDafDOk2lTf+gSllsDpvHknJ8/k2Ty3ZOd/cmjm95Jt3eDZPAJNOt6IDLddgyxePBr/AJdnms4C4L21sDuk+RXWwPd/hBRk+fwB/j2KWwy598GQHpGPdievAHXABfvQWazHpAZh+gv7i8g+5AWwPuNgxugoSJvuUkTeAJyJTKy0Snkmjfp6KR8EoYrJaAwUQ4omii/BQ1r0UVWT8D0ENCVVQFvYwEmJbCsIWsFDTFG12JocdgNd+Bp9gJiugHGr/AJGia9/YcXnZoUiuRL7+yav2NapBNVXasDi0iVlIvjnkIosrLKJvjfeiKecr/Q7a3aAtFtLFbGm++CMXa3kSfPxs0PRB4StCg/kjFunnfI4vG8AVt97+Bp5WfsSTvk1PdvaAt1JZpjc8duyoj1fot/HJqdqmKLqSyr1o2LpbdeiSel+5sZ2rbdotFre1hI61d9OL7kurC1/k6LTVPSAt1ZV+LMcv1fyicpfqO6tZt9winVT2c5V/dzyTbzen3Nbt2CF1cJ5YXVWsLjkDnhqvZl5wA23tOvyFv/8Aqn3D1PWb7UZJvXwSqTlarQG8Ush6svfkyUsLLoDXLX3yCUv09/R185ruwN5fawNlJXonNql+xrdLPwTm8LP3AxySzQJSxna1k1tO88AbrGP8EHSeF9ibeNWbN2uAPPkgxuwyfjRum23/ALJuwMbzT52F+DnjPcx4Cg3z8gloTbQWyKL7tch7ilb5BJ+CDH7+wZLnBrysUHnsBgWa8MLYBl/BjuhBdkAYXoUgPtyFGRORSX4JyyBORGRaRKeiaMgWjolHWSkBgrDZRcE4ascSikRxrmiazQ4/wDTQlXwDsNBCw/Al+QehKtlDX/eBL8oCdMQDT0OPcmnbQ08ANPgSdeiaeLzgcfBQ492UTxhWSQ0/ZRSL/wBjTe8OiSav2NNb0wyqm6rAk/NXyStV5KK6ra1sCkcra/yNSfCJRea34EroC0ZUnTTGm9f8yMfdCtvDKLdWUueBp57Z4IxlavszYu1TKK54xXI8a7Eou793g1SXKAqnccI1ypvD8kk+E8/sJN7boClpY2hYvzRJN7ePJ3U2u16Ar1vD0qO6lq8+STelbOUle3XoCrlbzSD1LCTp2TTu3nska5YarYDvGFr5MlJN6C5VaTtaD1U77AU6nbe6C3jGw3mn/wCA6s4f4AcpLH+A9u4XK6fAer7gJ13zeASleE7OxQJPn9OANk6aBJo6VXkLe65JoyTsDbS7WdJ55DJ4vZBkpd8aC+3B0m72qDJ5Ax/3MLefBsni37A3av74Csd1hhb7GumnTDLXsmqx5avAJXlY2a68hfsgx+wPvZr1e6MeqALf2C/Im90wsA2Y/kT9Ak2Qc9VoLN0gvXAAd0ZIT2wv9wqb/kEr2Nk5eHkCcuxKWmVnySkTR0XotDNEYFojBSJSOtE4awNaKKIUbsMRLGAaayan8BfY1PF/YISb5EthW7QlsBISeNhvnRq+ChpjTrQEsiiAr++xrZPUhxeQHF6Et4AsMSy/guBrxgaaeWTWjVw9MqLX+fwNNeLJJ0xRuvQRRO9NVWBqT2Si6Sq8CUsYoCqfGRrdcEur1a0anj+AK9SSzgXVjyTt9PAosoqpcfc3q8IinhMTb/kor1Wamk0ngknaedGp3WQK9Vv4wjm1zi+Sd2sujradq/AFU3blzRyedklLFUdetWBSTdXX+jnLOOcvIG3qwt2lnXkCnVTqrMu939sB6lezL7P5Abw7bzXIHdc7Mcth6n6AalinoN4tPRl1r7Ab/DATl9wvDrjkxu3wg9S7UiUa/Fhbp3f2MkwuWOxBzf2C3Tdo5vG0gtq7A5tU9PsCTZr/AABt16CsvLC3d/ua3SoLeV6Csfuw2uKNb4C+/YmjGq9hdPRt3rYW83qyDLx7C2978mt28BbrPAGWv9BzZstV/Bj0BjBnYm+5nBBnGAvBrMewC+1gYmGVhQl5Jy2UeycgJz7EpMpLVk56Jo6JWGiUNlYDBSJSOXsnHWykP+soavgeb7AQovgB1wKPnIfPBv2CF7NWFyH0JewEh2C8GrvdlDWPIl4AhcIBXmh4TxoC1zsSeuAGuBK65JrVCWCiiq8sSf3J2vliWUnooovNiTXdkrT+BrXsIdquRJqqwydtrj2OO92EOLvN2PqxrRJPGP8AwadO7AonhYNvvsmnvIrzlAUT4Xc6Mr3pcgVUd1AVUs3mjlpXZNZXdGuVqrLRTq/Tg236Jpu93R3Un2SAp1Y4s63nCRNS47mNrm9cAVb40YpL+AuVSwt+TLzWgE3i7xzjJ1pVnK1eguVPZlvy/ADTys/gLf3C5ZqzG7y9LsxQ7oMmG/lBk0sOiBN57GPZk3bWvIW++QNk83YW1WE2zm80Bv4A6XbbDLHY1tfIJZd2Fc3bMk5as5vGPugt2/AVzeeAt/Y6T32C9Nk0dh9zH37nN5rQW8+0Qc/DA9GtLIWB2L/cLef5N5DwBjpLAX2bNf4Mf8DQdmPk30ZwQFsw3Jz5ADAxvQX+AqcicvJRsEvAEpE5FJWTlu8E0dF2UiThXwUiMFI2ikWqBGhx8lDiNYQUJZ3oBK8GpUkzHfBqCEv2EgiW6sDY98CWbwFGrBcDXfJqyrCsGp5v9gGr0b8gToUa7UA07SNWmnlhjXyJPKAadc5En9yawvZuNVjsUUXtIUeKJ2+Rp4zsoSeN2JXkCfJqbxTCKJ1ixJ/4BF7OXCQFLxwzbTdgvSyanyEO7W86NeHuwJqlg1aygKWqOtXVMmt+TbtvjAFLzoy9LnwHqrjnudfd6wA+p6OTpetgvvTXk1ydAJun6Z15eAdTtLee5t0wN4v7nN5oN4MAd0w9VrGH4Dp4X2Mtb40ApeTHveA38oxvGQEnmmY3wY2tJhv8AJvPHcMnbdGXyg23a79wsa2qrIW87MlejGwrctoDffk1sLqwObz8h52bdeQp0Qc8+Qt+sG3nkLIMe+TLV5o564QXSwBj8GPvwa/27GMAt/Yzk1mPZBnwYzcth5YGP2Ya/JjALBJ15E6C6fIUH/JOQ5AlYE5E5lJ5vBKevBNGxKQJx8lIjBSHBSOETiUj4KGhp/IYiWwEajEb8gb8iVrgK2JayEb+TedchvIihLC2KNLAE2hIBLZu2FOxXQCt4TQl+AG26qgHGqF5JiWgGLm3tE1yhc0y4Gqs1t1lA2jU/S7FFE+xq/7IE7W7ZqqleuAKW6b+50XnLBf5FfsJDvK7+TU/jOfBNPJt7dgO7R3W+EBG3+qghp4buzk93kHvR3U64ApeVjgzCzWwxdvF7O6lV6AVq/27HJ2gt+Tm9cAK/B2+yXkKf2C35Ad3jwZechbdpM5t4/kK287M1GjL5dUY33foEL4C3m7yc27dGPTzYHJ8GXS1ZzlyHPVkK5tt2nZjr7mSeNGN/IHXm2Y9rB1urvQW74INfKp4BybL7GXn/YHaRjb4MfJj/wC8kGMx97Oe2YBwXnGjX2MAyTC/uazPbogxvijHhbN41QfAGdzHRuDHkKLDITXlBeQBLZOQ5aBLNoCctE5FJEp8mRsaaKQJw0UiUUiUiTiON8lFI2tfkS2GL7ZYt6AZqoK9GpgJaNQV/wBYl2CFd0czFV+RZ5oDc6NXgxY2b+ChcZNXIe5qASecG3umFawasrgBr+TU28gvhCj+AEnyKOQezVvv2KGni6NTb2sht9jrq6eQKJ+bWjb0/wBiaeRX5KGnu2jdvVAujdIBx7qjr9Avu0cvHID2jlugrnR15zsB9WPBzbtL+Q3fbB2VzYDcneas6+LJ9XnQm9+QNTa8PZrfnQG/JyedgNvyFNXkxvnvkxtJ7/ICbzVnN+Q2zM1wArz4MvOTHtHN+AOi82davNPkN4paOffsBzMb/wC7nN5M0t/JB1v0Y3R0nS/0Y2rq8gc/GwvH2Nxw7yEg6852Y7vuc952Zi/3A5sLzeaNfNhvDQHSfbuHzRrfcy+yAw55R37BbxoDn4Df7ie8mNeiDOwfKo32ZwB3AUbnlBCsfh2CWxtgfkAS9E2UkTlrgASJyKSJS5IOjwVROOikSBx7FF+4I+xo0HEaJr8jQCWrsSvdhXo3tQDTpaOTDeRL2E1osh5N54AW1hmp8NBRv2KFzgSXnILo28gJVyankxayuDeQNW+BJ/FAt2at2A/Rt5VbAmuBJ+sgbGvkSePz6BZtvlgLq4rQll8ATo6yiieKVG9RN7NtJAO1e1o3dpfAOOTurF/Yoo39zHwG2cn6sBOVP0a2g3o68+QEnk26fcDavydeMrACyrwdaBav33NsBWtcmN5oNpHXoBW6O7chTozC5AWarRl7r7GN4yZecEoV2tozx57mXjSRjdrWxRss6MisYMtWuxlunwQa2dzQW+TLzYGsy37OszOQOejLzeLObxWAgcsrgxmtsL+ANuv8Bec6NeaozF0/gDOH/JjdmtmMDHlGM5vtgwg5vZl1Z3GK+5ngDn2oxrto57CwrH5C/wCRPOwaAEtgluxyAwBInPuUeycjIyOisfySRSO6ApFjjwwRHFmg0xfIFvY4sBIS5yBe8i5AS+DUF+DfkBL2bYVwsGrYQrNYcCW8oDb7I5M70jlooV6v9jYh5yatALto3mgm75AS8G28ch5zg1ZAVq8He2G1fwbxkBt7OvIUzUwEcmg3jWTk0wGsO/sdf/gbfg68eShXWcmp1kGzrr/QDx9zb8gtujk9aFDT7mJ/uF3wzk32ASt/+nW7dmc2Z4AV/Bzxuwt3vsdkDW9nLvaz5Cde/wAEC1WTrV2gWde6A3xg5vBjZifcBa7eTLv/AEZfozgDW/BkuTHr0zufRRr7Ug7MfwdjZBt+jLMzejLQGmN/NeDPuY8Ac67mYN80Y8UBzf8AszwdZjecEHZpMx+EdeMaCBrfwY/JzMboKxmeKOZmvIGPWgs1+gvYBb4oDGwMCbBIcvJORNHR7FFROHA46IKIoqrROOBo0HH0JegciVAP8G8hNTASfYWtAWMp2JMDTdsN4sS+ANxo3b4oN4NT8gJVZt5bYV3OQQ9u9o5YXYK8M3FFCTfJuAbEn3AXK2cn/wAgmgK1/wAzerwC38HX9wKfB15wC7bNumAr7GqrrgCZt/YB3izr/IDk+LsB33OWdhbOT7ANa8+DFgN5ZwDvuZeNh6k8HWqAd+TLC33OtPv5ATef9nWC3Xo285f5AT9bOT8Bt34M42A77GWu1Bbwc9gannRt4argNqrMvYCvLZhl+TLp+ANvuztmGX5A2+MeTP3OvSMbf3A6zm+TL8meawBpj2dirMdAc/ZzMWzsEGaOMetHc2Bnbuc37Msx1eMhWh2aw3kDm8UZycdwBkvkLZrewyYBl2BLuOX5BL4AnInLWikicngyOjhFIkovCwUiBSI13snF2NM0HEQEJegGnybYPWjrAfoSYPB1vhgUTxs2/ZNOzbzWwKJm35J2beP4Ap8nWC/Zt0A/yanwid4rg1vsA7ODZ3VyA7zeUbfILOQqHehWtfYlfBt8Wy0Nbya3igKR3UA3RvtgTOu0A7bydaqgNnWA75Nvuybfk6wKXSMvH7MNv0ZYFLyZf/IF14NvhgNvFnX9ifV3Z112Ad8G20TvR1gNtXk6wX3MTYFLvHPB106B1Zyc28gLawdxkCeLb5O6vgBtmMLfwZbyA2zLew3xk68ijbebZ15DeGjLJQ78mN9wtmN+AQm+O5j1tmdRjeUwpN5MeeQt50c5AaYzG/JjfbYG5rKMv0dYbA1+DDLMsBWg35OboxuwObC8nWYBjYJd0bIDfJBkichyJz5IMiOJKDwNAWXsSZKLGn2KKJ5NvgCZqZRSzrBZt32oBp2amC/R10gKJ9jrBaOsCl6NTJpnWBVs1OiXV5NvIFE8G98k1LJ3VywKp+Tr+SfV3wd1AU6uDU6J3k66bApzRqZLq2zk/IFbycn5Jp42d1Y8gUukb1ckro1StYAp1eTuon1a9HX9gKWtGuXcl1cndQFbzVnN0Sv4N6sbAopYt7Mu8WDq7HX5oEUcsndT1ZLqOcrQFWzOry/sTUrO6vXkCvVj0YpE+pcHX5Ap1eTur0T6snWBTqOvJNO+x1/8wHbSOsnfqzr+4Dv3Z15wTv7ndQFG1jt3MvOANmdS7gUsywNnWA7DYepfJnVyA2+x3UT6jm/ID6kY5YA5Z2Y5AO/LOb4BZlgOzLDZl+QE35Okw2Y2Am8YC3+TLC2BrYGzm6C2QY3sEtCbyTkyAxY0+5JNjTAqmJPkkmJMCqZqeCXUb1AVT4O6u5NPybZRTqNvRLqO6qFFeo7qzsnZ18iil9jVIn1I5SFFOruapEuqsm9X2Ar1G9XkipGqRRVSs3qI3g3qyBXqNUiPVnJ3UBZSXya2Q6jeoCylRyZLqO6/YFXI7q4JdR3VfKAr1ZOsl1LR3VgCt5N6iPVg5SAr1e0d1cIl1eDurAFerB3Vgl1HdWdgVs7q8kuo5yoCvUb1cWR6juqgK9WTupkuo7qAr1HdRLq4O6uQK9R3VuyPV8HOQFurRnUTcuzM6sAVcjm71gi5muXkCnUY2TUjOpAV6tndRJyOcuQKN9zuok5HWBS/JnV/sHUZ1Z7EFbyZaJ9RnVa2KKN+8ndWP3J9Wd4O6hQ7OvGifVk7qFD6jGwWdYobZjl5D1BbIFYWzG8BbA5sEma2CTAKfYSZxwCT8mqRxwGpmpnHAapHdRxwG9WNnXk44DrNUjjgO6jupdzjgO6vKO6vJxwG9Xdm9XY44DOo1SOOA7qVHdRxxRvVk7qOOA3qSp2d1nHAd1HdRxxB3Ud1HHFHOR3UccBrkd1YOOFHKR3V5OOIO6jOo44DnJHdZxxR3X5O6kccKOUvJ3V5OOA7q5O6s9zjgM6+LO6jjhR3XyZ1HHEHdXk7qOOAzqN6jjgM6vJ3UccB3Uc5HHAZ1HdRxwHWrMckccB1ndRxwGdR3VwccBnUY2ccBjZjkccAWwtnHAf/2Q==",
                        IsVerifiedPage = false,
                        IsConfirm = false,
                    };
                    var result = await _userManager.CreateAsync(user, signUpModel.AccountPassword);
                    string errorMessage = null;
                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync(RoleModel.SuperAdmin.ToString()))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(RoleModel.SuperAdmin.ToString()));
                        }
                        if (await _roleManager.RoleExistsAsync(RoleModel.SuperAdmin.ToString()))
                        {
                            await _userManager.AddToRoleAsync(user, RoleModel.SuperAdmin.ToString());
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
        public async Task<ResponeModel> SignUpAdminAccount(SignUpModel signUpModel)
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
                        Avatar = "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",
                        BackgroundImg = "/9j/4AAQSkZJRgABAQEBLAEsAAD/4QBWRXhpZgAATU0AKgAAAAgABAEaAAUAAAABAAAAPgEbAAUAAAABAAAARgEoAAMAAAABAAIAAAITAAMAAAABAAEAAAAAAAAAAAEsAAAAAQAAASwAAAAB/+0ALFBob3Rvc2hvcCAzLjAAOEJJTQQEAAAAAAAPHAFaAAMbJUccAQAAAgAEAP/hDIFodHRwOi8vbnMuYWRvYmUuY29tL3hhcC8xLjAvADw/eHBhY2tldCBiZWdpbj0n77u/JyBpZD0nVzVNME1wQ2VoaUh6cmVTek5UY3prYzlkJz8+Cjx4OnhtcG1ldGEgeG1sbnM6eD0nYWRvYmU6bnM6bWV0YS8nIHg6eG1wdGs9J0ltYWdlOjpFeGlmVG9vbCAxMC4xMCc+CjxyZGY6UkRGIHhtbG5zOnJkZj0naHR0cDovL3d3dy53My5vcmcvMTk5OS8wMi8yMi1yZGYtc3ludGF4LW5zIyc+CgogPHJkZjpEZXNjcmlwdGlvbiByZGY6YWJvdXQ9JycKICB4bWxuczp0aWZmPSdodHRwOi8vbnMuYWRvYmUuY29tL3RpZmYvMS4wLyc+CiAgPHRpZmY6UmVzb2x1dGlvblVuaXQ+MjwvdGlmZjpSZXNvbHV0aW9uVW5pdD4KICA8dGlmZjpYUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpYUmVzb2x1dGlvbj4KICA8dGlmZjpZUmVzb2x1dGlvbj4zMDAvMTwvdGlmZjpZUmVzb2x1dGlvbj4KIDwvcmRmOkRlc2NyaXB0aW9uPgoKIDxyZGY6RGVzY3JpcHRpb24gcmRmOmFib3V0PScnCiAgeG1sbnM6eG1wTU09J2h0dHA6Ly9ucy5hZG9iZS5jb20veGFwLzEuMC9tbS8nPgogIDx4bXBNTTpEb2N1bWVudElEPmFkb2JlOmRvY2lkOnN0b2NrOmU5NDVkMzA4LTk4YjItNGM3Mi1iZDhhLWUxYzg5ZDE2N2JjMzwveG1wTU06RG9jdW1lbnRJRD4KICA8eG1wTU06SW5zdGFuY2VJRD54bXAuaWlkOmMyNjgxNTE4LTZmMmEtNGExYi05ODUwLWJiMjM0NWE1NjIyZDwveG1wTU06SW5zdGFuY2VJRD4KIDwvcmRmOkRlc2NyaXB0aW9uPgo8L3JkZjpSREY+CjwveDp4bXBtZXRhPgogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAogICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgCiAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAKICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgICAgIAo8P3hwYWNrZXQgZW5kPSd3Jz8+/9sAQwAFAwQEBAMFBAQEBQUFBgcMCAcHBwcPCwsJDBEPEhIRDxERExYcFxMUGhURERghGBodHR8fHxMXIiQiHiQcHh8e/9sAQwEFBQUHBgcOCAgOHhQRFB4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4eHh4e/8AAEQgBaAJAAwEiAAIRAQMRAf/EABoAAQEBAQEBAQAAAAAAAAAAAAIDAQAEBQj/xAAuEAACAgIBBAEDAwUBAQEBAAAAAQIRITFBElFhcYEDkaEiscETMtHh8PFSQmL/xAAWAQEBAQAA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",
                        IsVerifiedPage = false,
                        IsConfirm = false,
                    };
                    var result = await _userManager.CreateAsync(user, signUpModel.AccountPassword);
                    string errorMessage = null;
                    if (result.Succeeded)
                    {
                        if (!await _roleManager.RoleExistsAsync(RoleModel.Admin.ToString()))
                        {
                            await _roleManager.CreateAsync(new IdentityRole(RoleModel.Admin.ToString()));
                        }
                        if (await _roleManager.RoleExistsAsync(RoleModel.Admin.ToString()))
                        {
                            await _userManager.AddToRoleAsync(user, RoleModel.Admin.ToString());
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

        public async Task<ResponeModel> UpdateAvatar(UpdateAvatarModel avatar, string userId)
        {
            try
            {
                var existingAccount = await _context.People.FirstOrDefaultAsync(a => a.Id == userId);

                if (existingAccount == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Account not found" };
                }

                existingAccount = submitAvatarChanges(existingAccount,avatar);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Account profile updated successfully", DataObject = existingAccount };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updating the account profile" };
            }
        }
        private Person submitAvatarChanges(Person account, UpdateAvatarModel updateAvatar)
        {
            account.Avatar = updateAvatar.Avatar;
            return account;
        }

        public async Task<ResponeModel> UpdateBackGround(UpdateBackGroundModel backGround, string userId)
        {
            try
            {
                var existingAccount = await _context.People.FirstOrDefaultAsync(a => a.Id == userId);

                if (existingAccount == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Account not found" };
                }

                existingAccount = submitBackGroundChanges(existingAccount, backGround);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Account profile updated successfully", DataObject = existingAccount };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updating the account profile" };
            }
        }
        private Person submitBackGroundChanges(Person account, UpdateBackGroundModel updateBackGround)
        {
            account.BackgroundImg = updateBackGround.BackgroundImg;
            return account;
        }
        public async Task<ResponeModel> GetAllAccountBySuperAdmin()
        {
            try
            {
                // Assume your user model has a property for roles
                var accounts = await _userManager.Users.ToListAsync();

                var nonSuperAdminAccounts = accounts.Where(u =>
                    !_userManager.IsInRoleAsync(u, "SuperAdmin").Result
                      ).ToList();
                // Transform the user data to include roles
                var usersWithRoles = new List<object>();

                foreach (var user in nonSuperAdminAccounts)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    usersWithRoles.Add(new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.Address,
                        user.Gender,
                        user.PhoneNumber,
                        user.Dob,
                        user.FullName,
                        user.DateUserRe,
                        user.Status,
                        Roles = roles
                    });
                }

                return new ResponeModel { Status = "Success", Message = "Accounts Found", DataObject = usersWithRoles };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return new ResponeModel { Status = "Error", Message = "An error occurred while find the account " };
            }
        }
        public async Task<ResponeModel> GetAllAccountByAdmin()
        {
            try
            {
                // Assume your user model has a property for roles
                var accounts = await _userManager.Users.ToListAsync();

                // Filter out accounts with SuperAdmin role
                var nonSuperAdminAccounts = accounts.Where(u => !_userManager.IsInRoleAsync(u, "Admin").Result 
                && !_userManager.IsInRoleAsync(u, "SuperAdmin").Result).ToList();

                var usersWithRoles = new List<object>();

                foreach (var user in nonSuperAdminAccounts)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    usersWithRoles.Add(new
                    {
                        user.Id,
                        user.UserName,
                        user.Email,
                        user.Address,
                        user.Gender,
                        user.PhoneNumber,
                        user.Dob,
                        user.FullName,
                        user.DateUserRe,
                        user.Status,
                        Roles = roles
                    });
                }
                // Perform any additional processing or transformation if needed

                return new ResponeModel { Status = "Success", Message = "Account Found", DataObject = usersWithRoles };
            }
            catch (Exception ex)
            {
                // Log the exception or handle it as needed
                return new ResponeModel { Status = "Error", Message = "An error occurred while find the account " };
            }
        }
        public async Task<ResponeModel> GetAllAccountCreateInMonth(int year, int month)
        {
            var startDate = new DateTime(year, month, 1);
            var endDate = startDate.AddMonths(1).AddDays(-1);

            var countAccount = await _context.People
                .Where(c => c.DateUserRe >= startDate && c.DateUserRe <= endDate)
                .ToListAsync();

            if (countAccount.Count > 0)
            {
                int sumAccount = countAccount.Count();
                return new ResponeModel { Status = "Success", Message = "Account Found ", DataObject = sumAccount };
            }
            else
            {
                return new ResponeModel { Status = "Error", Message = "Not Found ", DataObject = 0 };
            }
        }


        public async Task<ResponeModel> UpdateUserRole(string userId, string selectedRole)
        {
            try
            {
                var existingUser = await _userManager.FindByIdAsync(userId);

                if (existingUser == null)
                {
                    return new ResponeModel { Status = "Error", Message = "User not found" };
                }

                // Check if the user is a SuperAdmin
                var isSuperAdmin = await _userManager.IsInRoleAsync(existingUser, RoleModel.SuperAdmin.ToString());

                if (isSuperAdmin)
                {
                    return new ResponeModel { Status = "Info", Message = "SuperAdmin cannot be changed to other roles" };
                }

                // Check if the selected role is valid
                if (!Enum.TryParse<RoleModel>(selectedRole, out var roleModel))
                {
                    return new ResponeModel { Status = "Error", Message = "Invalid role selection" };
                }

                // Check if the selected role is the same as the user's current role
                if (await _userManager.IsInRoleAsync(existingUser, roleModel.ToString()))
                {
                    return new ResponeModel { Status = "Info", Message = $"User is already in the {roleModel} role" };
                }

                // Remove the user from all existing roles
                var existingRoles = await _userManager.GetRolesAsync(existingUser);
                await _userManager.RemoveFromRolesAsync(existingUser, existingRoles);

                // Check if the selected role exists, if not, create it
                if (!await _roleManager.RoleExistsAsync(roleModel.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleModel.ToString()));
                }

                // Add the user to the selected role
                await _userManager.AddToRoleAsync(existingUser, roleModel.ToString());

                return new ResponeModel { Status = "Success", Message = $"User role updated to {roleModel} successfully", DataObject = existingUser };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updating user role" };
            }
        }

        public async Task<ResponeModel> ForgetPasswordAsync(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null )
                {
                    // Không tìm thấy tài khoản hoặc email chưa được xác thực
                    return new ResponeModel { Status = "Error", Message = "Invalid email or email not confirmed" };
                }

                // Tạo mã xác thực ngẫu nhiên (ví dụ: 5 số)
               // var code = await GenerateRandomCodeAsync();
                var code = await _userManager.GenerateUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword");
                // Gửi email xác thực
                await SendResetPasswordEmailAsync(user, code);

                return new ResponeModel { Status = "Success", Message = "Reset password email sent successfully" };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while processing the request" };
            }
        }


        private async Task SendResetPasswordEmailAsync(Person user, string code)
        {
            var emailSettings = configuration.GetSection("EmailSettings");

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(emailSettings["SenderName"], emailSettings["SenderEmail"]));
            message.To.Add(new MailboxAddress(user.Email, user.Email));
            message.Subject = emailSettings["ResetPasswordSubject"];

            // Nội dung email
            var confirmationLink = $"{emailSettings["ConfirmationLink"]}?code={code}&email={user.Email}";

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = $@"
            <!DOCTYPE html>
            <html lang=""en"">

            <head>
                <meta charset=""UTF-8"">
                <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
            </head>

            <body>
                <div id=""wrapper"" style=""display: flex; align-items: center; justify-content: center; margin: 0px auto; max-width:600px;"">
                    <div class=""form"" style=""width: 600px; max-height: 450px; border: 1px solid black; border-radius: 10px;"">
                        <div class=""header"" style=""border: 1px solid gray; border-radius: 10px; background-color: #E65555;"">
                            <div class=""logo"" style=""height: 90px; width: 200px; background-color: none; margin: 10px auto; border-radius: 10px; display: flex; align-items: center; justify-content: center;"">
                                <img src=""https://static-00.iconduck.com/assets.00/pinterest-icon-497x512-g88cs2uz.png"" alt=""Logo"" srcset="""" style=""width: 100px; height: 100%; margin: 0 auto;"">
                            </div>
                            <div class=""text"" style=""text-align: center;"">
                                <h1 style=""color: white"">Chào mừng bạn đến với ASM</h1>
                                <div class=""link"" style=""width: 150px; height: 30px; background-color: aliceblue; border-radius: 5px; margin: 0 auto 10px; display: flex; align-items: center; justify-content: center; border: 1px solid black;"">
                                    <a href='{confirmationLink}' style=""text-decoration: none; margin: 5px auto 0; color: black;"">Thay đổi mật khẩu
        
    </a>
                                
                                </div>
                            </div>
                        </div>

                        <div class=""footer"" style=""display: flex; align-items: center; justify-content: center;"">
                            <div class=""footercontent"" style=""width: 480px; text-align: center; margin: 0px auto;"">
                                <div class=""span1"" style=""margin-top: 10px; margin-bottom: 10px;"">
                                    <span>Copyright © 2024 ASP, Allrights reserved.</span>
                                </div>
                                <div><b>Địa chỉ của chúng tôi:</b></div>
                                <div class=""span2"" style=""margin-top: 10px;"">
                                    <span>C11-01, số 473 Man Thiện, Thủ Đức, KDT Geleximco Lê Trọng Tấn, Phường Dương Nội, Quận Hà Đông, Hà Nội</span></div>
                                <div class=""span3"" style=""margin-top: 10px;"">
                                    <span>Email: minhtam250102@gmail.com</span>
                                </div>
                                <div class=""span4"" style=""margin-top: 10px;""><span>Hotline: 0979500611</span></div>
                            </div>
                         </div>
                      </div>
                   </div>
               </body>
            </html>";
            message.Body = bodyBuilder.ToMessageBody();

            // Gửi email
            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                await client.ConnectAsync(emailSettings["SmtpServer"], Convert.ToInt32(emailSettings["SmtpPort"]), false);
                await client.AuthenticateAsync(emailSettings["SmtpUsername"], emailSettings["SmtpPassword"]);
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task<ResponeModel> ConfirmResetPasswordAsync(string email, string code, string newPassword)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);

                if (user == null)
                {
                    return new ResponeModel { Status = "Error", Message = "User not found" };
                }

                // Kiểm tra xác thực mã sử dụng DataProtectionTokenProvider
                var isCodeValid = await _userManager.VerifyUserTokenAsync(user, _userManager.Options.Tokens.PasswordResetTokenProvider, "ResetPassword", code);
              //  var isCodeValid = code == await GenerateRandomCodeAsync();
                if (!isCodeValid)
                {
                    return new ResponeModel { Status = "Error", Message = "Invalid verification code" };
                }

                // Xác nhận mã thành công, thay đổi mật khẩu
                var resetPasswordResult = await _userManager.ResetPasswordAsync(user, code, newPassword);

                if (resetPasswordResult.Succeeded)
                {
                    return new ResponeModel { Status = "Success", Message = "Password reset successfully" };
                }
                else
                {
                    return new ResponeModel { Status = "Error", Message = "Failed to reset password" };
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while processing the request" };
            }
        }

        public async Task<ResponeModel> BanAccount(string userId)
        {
            try
            {
                var existingAccount = await _context.People.FirstOrDefaultAsync(a => a.Id == userId);

                if (existingAccount == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Account not found" };
                }

                existingAccount = UpdateStatusAccount(existingAccount);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Account updated successfully", DataObject = existingAccount };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updated Account" };
            }
        }
        private Person UpdateStatusAccount(Person person)
        {
            person.Status = false;
            return person;
        }

        public async Task<ResponeModel> UnBanAccount(string userId)
        {
            try
            {
                var existingAccount = await _context.People.FirstOrDefaultAsync(a => a.Id == userId);

                if (existingAccount == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Account not found" };
                }

                existingAccount = UpdateStatusUnBanAccount(existingAccount);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Account updated successfully", DataObject = existingAccount };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updated Account" };
            }
        }
        private Person UpdateStatusUnBanAccount(Person person)
        {
            person.Status = true;
            return person;
        }

        public async Task<ResponeModel> UpdateIsConfirm(string userId)
        {
            try
            {
                var existingAccount = await _context.People.FirstOrDefaultAsync(a => a.Id == userId);

                if (existingAccount == null)
                {
                    return new ResponeModel { Status = "Error", Message = "Account not found" };
                }

                existingAccount = UpdateStatusIsConfirm(existingAccount);

                await _context.SaveChangesAsync();

                return new ResponeModel { Status = "Success", Message = "Account updated successfully", DataObject = existingAccount };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
                return new ResponeModel { Status = "Error", Message = "An error occurred while updated Account" };
            }
        }

        private Person UpdateStatusIsConfirm(Person person)
        {
            person.IsVerifiedPage = true;
            return person;
        }
    }
}
