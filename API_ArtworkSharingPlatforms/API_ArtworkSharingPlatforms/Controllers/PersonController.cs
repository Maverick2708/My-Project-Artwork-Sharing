using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;

namespace API_ArtworkSharingPlatforms.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : Controller
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        [HttpPost("SignUpPerson")]
        public async Task<ActionResult> SignUp(SignUpModel signUpModel)
        {
            if (ModelState.IsValid) 
            {
                var result = await _personService.SignUpAccountAsync(signUpModel);
                if(result.Status.Equals("Success"))
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return ValidationProblem(ModelState);
        }

        [HttpPost("ConfirmEmailAsync")]
        public async Task<IActionResult> ConfirmEmailAsync(string email, string token)
        {
            var response = await _personService.ConfirmEmailAsync(email, token);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }


        [HttpGet("email")]
        public async Task<ActionResult<PersonModel>> GetPersonByEmail(string email)
        {
            var data = await _personService.GetPersonByEmail(email);
            if(data != null)
            {
                return Ok(data);
            }
            else
            {
                return NotFound();
            }
        }
        [HttpPost("SignIn")]
        public async Task<ActionResult> SignInAccountAsync(SignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _personService.SignInAccountAsync(signInModel);
                if (result.Status.Equals(false))
                {
                    //return Unauthorized();
                    ModelState.AddModelError(string.Empty, result.Message);
                    return ValidationProblem(ModelState);
                }
                return Ok(result);
            }
            return ValidationProblem(ModelState);
        }

        [HttpPut("UpdateVerifiedPage")]
        public async Task<IActionResult> UpdateVerifiedPage(string userId)
        {
            var response = await _personService.UpdateVerifiedPage(userId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateAccount")]
        public async Task<IActionResult> UpdateAccount(UpdateProfileModel updateProfileModel, string userId)
        {
            var response = await _personService.UpdateAccount(updateProfileModel,userId);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("ChangePasswordAsync")]
        public async Task<IActionResult> ChangePasswordAsync(ChangePasswordModel changePassword)
        {
            var response = await _personService.ChangePasswordAsync(changePassword);
            if (response.Status == "Error")
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken(TokenModel tokenModel)
        {
            var response = await _personService.RefreshToken(tokenModel);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("SignUpSuperAdminAccount")]
        public async Task<ActionResult> SignUpSuperAdminAccount(SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _personService.SignUpSuperAdminAccount(signUpModel);
                if (result.Status.Equals("Success"))
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return ValidationProblem(ModelState);
        }

        [HttpPost("SignUpAdminAccount")]
        public async Task<ActionResult> SignUpAdminAccount(SignUpModel signUpModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _personService.SignUpAdminAccount(signUpModel);
                if (result.Status.Equals("Success"))
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return ValidationProblem(ModelState);
        }

        [HttpPut("UpdateAvatar")]
        public async Task<IActionResult> UpdateAvatar(UpdateAvatarModel avatar, string userId)
        {
            var response = await _personService.UpdateAvatar(avatar,userId);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateBackGround")]
        public async Task<IActionResult> UpdateBackGround(UpdateBackGroundModel backGround, string userId)
        {
            var response = await _personService.UpdateBackGround(backGround, userId);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllAccountBySuperAdmin")]
        public async Task<IActionResult> GetAllAccountBySuperAdmin()
        {
            var response = await _personService.GetAllAccountBySuperAdmin();
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllAccountByAdmin")]
        public async Task<IActionResult> GetAllAccountByAdmin()
        {
            var response = await _personService.GetAllAccountByAdmin();
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("GetAllAccountCreateInMonth")]
        public async Task<IActionResult> GetAllAccountCreateInMonth(int year, int month)
        {
            var response = await _personService.GetAllAccountCreateInMonth(year,month);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }


        [HttpPut("UpdateUserRole")]
        public async Task<IActionResult> UpdateUserRole(string userId, string selectedRole)
        {
            var response = await _personService.UpdateUserRole(userId, selectedRole);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpPost("ForgetPasswordAsync")]
        public async Task<IActionResult> ForgetPasswordAsync([FromBody] ForgotPasswordRequestModel model)
        {
            var result = await _personService.ForgetPasswordAsync(model.Email);
            if (result.Status == "Success")
            {
                return Ok(new { Message = result.Message });
            }
            else
            {
                return BadRequest(new { Message = result.Message });
            }
        }

        [HttpPost("ConfirmResetPasswordAsync")]
        public async Task<IActionResult> ConfirmResetPasswordAsync(string email, string code, string newPassword)
        {
            var response = await _personService.ConfirmResetPasswordAsync(email, code,newPassword);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("BanAccount")]
        public async Task<IActionResult> BanAccount(string userId)
        {
            var response = await _personService.BanAccount(userId);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("UnBanAccount")]
        public async Task<IActionResult> UnBanAccount(string userId)
        {
            var response = await _personService.UnBanAccount(userId);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);
        }

        [HttpGet("UpdateIsConfirm")]
        public async Task<IActionResult> UpdateIsConfirm(string userId)
        {
            var response = await _personService.UpdateIsConfirm(userId);
            if (response.Status.Equals(false))
            {
                return Conflict(response);
            }
            return Ok(response);

        }
    }
}
