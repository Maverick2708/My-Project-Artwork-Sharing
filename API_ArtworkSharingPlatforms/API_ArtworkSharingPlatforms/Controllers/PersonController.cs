using API_ArtworkSharingPlatform.Repository.Data;
using API_ArtworkSharingPlatform.Services.Interfaces;
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
    }
}
