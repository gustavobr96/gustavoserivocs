using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sistema.Bico.Domain.Generics.Entities;
using Sistema.Bico.Domain.Generics.Interfaces;
using SistemaBico.API.Configurations;
using System.Threading.Tasks;

namespace Sistema.Bico.API.Controllers
{
    [ApiController]
    [Route("v{version:apiVersion}/api/token")]
    public class TokenController : Controller
    {

        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IToken _token;

        public TokenController(SignInManager<ApplicationUser> signInManager,
           UserManager<ApplicationUser> userManager,
           IToken token)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _token = token;
        }


        [AllowAnonymous]
        [HttpPost("CreateToken")]
        public async Task<IActionResult> CreateToken([FromBody] InputModel Input)
        {
            if (string.IsNullOrWhiteSpace(Input.Email) || string.IsNullOrWhiteSpace(Input.Password))
                return Unauthorized();

            var result = await _signInManager.PasswordSignInAsync(Input.Email, Input.Password, false, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return Ok(await _token.GerarJwt(Input.Email));
            }
            else
            {
                return Unauthorized();
            }
        }

    }
}
