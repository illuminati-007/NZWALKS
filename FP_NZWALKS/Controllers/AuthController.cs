using FP_NZWALKS.Models.DTO;
using FP_NZWALKS.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.AccessControl;

namespace FP_NZWALKS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ITokenRepository tokenRepository;

        public AuthController(UserManager<IdentityUser> userManager, ITokenRepository tokenRepository)
        {
            this.userManager = userManager;
            this.tokenRepository = tokenRepository;
        }

        //POST: /api/Auth/Register
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username
            };
            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)
            {
                // Add roles to this user
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);
                    if (identityResult.Succeeded)
                    {
                        return Ok("User was Registered!Please Login.");
                    }
                }
            }
            return BadRequest("Something went wrong");
        
        }


        [HttpPost]
        [Route("Login")]
        //POST: /api/Auth/Login

        public async Task<IActionResult> Login([FromBody]LoginRequestDto loginRequestDto)
        {
            var user =await userManager.FindByEmailAsync(loginRequestDto.Username);
            if(user != null) 
            { 
             var checkPasswordResult = await userManager.CheckPasswordAsync(user, loginRequestDto.Password);
                if (checkPasswordResult)
                {

                    var roles = await userManager.GetRolesAsync(user);
                    if (roles != null)
                    {


                        //create token
                        var jwtToken= tokenRepository.CreateJwtToken(user, roles.ToList());
                        var responce = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                        };
                        return Ok(responce);
                    } 
                }
            }
            return BadRequest("Incorect username or password!");
        }
    }
}
