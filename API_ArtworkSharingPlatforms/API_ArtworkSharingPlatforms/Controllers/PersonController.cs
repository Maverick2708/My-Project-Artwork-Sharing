using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Repository.Models;
using API_ArtworkSharingPlatform.Services.Interfaces;
using API_ArtworkSharingPlatform.Services.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
                    return Unauthorized();
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
    }
}
