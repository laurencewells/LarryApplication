using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LarryApplication.API.Data;
using LarryApplication.API.Dtos;
using LarryApplication.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace LarryApplication.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDtos userForRegisterDtos)
        {
            //validate request
            userForRegisterDtos.Username = userForRegisterDtos.Username.ToLower();

            if(await _repo.UserExists(userForRegisterDtos.Username))
                return BadRequest("Username already exists");

            var userToCreate = new User();
            userToCreate.Username = userForRegisterDtos.Username;
            var createdUser = await _repo.Register(userToCreate, userForRegisterDtos.Password);
           
            return StatusCode(201);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDtos userForLoginDtos)
        {
            var userFromRepo = await _repo.Login(userForLoginDtos.Username.ToLower(), userForLoginDtos.Password);
            
            if(userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)


            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptior = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptior);

            return Ok (new{
                token = tokenHandler.WriteToken(token)
            });




        }

    }
}