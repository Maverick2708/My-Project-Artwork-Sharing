using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Interfaces;
using API_ArtworkSharingPlatform.Repository.Models;
using AutoMapper;
using Microsoft.AspNetCore.Http;
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
                        Avatar = null,
                        BackgroundImg = null,
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

                var person = await _context.People.FirstOrDefaultAsync(p=> p.UserName == signInModel.AccountEmail && p.Status == true);

                if (user != null && person != null )
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
                    return new AuthenticationResponseModel
                    {
                        Status = false,
                        Message = "Account is inactive. Please contact support."
                    };
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
            //account.Avatar = updateProfileModel.Avatar;
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
                        Avatar = null,
                        BackgroundImg=null,
                        IsVerifiedPage = false,
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
                        Avatar = null,
                        BackgroundImg = null,
                        IsVerifiedPage = false,
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

                // Filter out accounts with SuperAdmin role
                var nonSuperAdminAccounts = accounts.Where(u => !_userManager.IsInRoleAsync(u,"SuperAdmin").Result).ToList();

                // Perform any additional processing or transformation if needed

                return new ResponeModel { Status = "Success", Message = "Account Found",DataObject= nonSuperAdminAccounts };
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

                // Perform any additional processing or transformation if needed

                return new ResponeModel { Status = "Success", Message = "Account Found", DataObject = nonSuperAdminAccounts };
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
    }
}
