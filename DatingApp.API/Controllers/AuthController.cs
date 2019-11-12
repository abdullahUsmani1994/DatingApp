using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.Data;
using DatingApp.API.Dtos;
using DatingApp.API.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;

            _repo = repo;
        }
        [HttpPost("register")]

        public async Task<IActionResult> Register(UserForRegistrationDto userForRegistration)
        {
            
            userForRegistration.Username = userForRegistration.Username.ToLower();

            if (await _repo.UserExists(userForRegistration.Username))
                return BadRequest("Username already exists");

            var userToCreate = new UserModel
            {
                Username = userForRegistration.Username
            };
            var createdUser = await _repo.Register(userToCreate, userForRegistration.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]

        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {
           
            var userFromrepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password.ToLower());
            if (userFromrepo == null)
                return Unauthorized();

            // The user requests an action. The relying party (RP) application asks for a token.
            // The user presents the credentials to the issuing authority that the RP application trusts.
            // The issuing authority issues a signed token with claims, after authenticating the user’s credentials.
            // The user presents the token to the RP application. The application validates the token signature, extracts the claims, and based on the claims, either accepts or denies the request.
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromrepo.Id.ToString()), // token for user Id 
                new Claim(ClaimTypes.Name, userFromrepo.Username) // token for user Name
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8
            .GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature); // signing credentials

            var tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddMinutes(2),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new {
                token= tokenHandler.WriteToken(token)
            });


        }

    }
}