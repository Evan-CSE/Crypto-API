using BlogWebsite.Data;
using BlogWebsite.Model;
using BlogWebsite.Model.Entities;
using BlogWebsite.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlogWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(DbContextEntity dbContextEntity, IConfiguration configuration) : ControllerBase
    {
        private readonly IUserRepository _userRepository = new UserRepository(dbContextEntity);
        private readonly IConfiguration _configuration = configuration;

        [HttpPost]
        [Route("/login")]
        public IActionResult Login (UserLoginDto loginDto)
        {
            var maybeUser = _userRepository.GetUserByUserNameAndPassword(loginDto);

            if (maybeUser == null)
            {
                return NotFound();
            }

            var userToken = GenerateToken(maybeUser);

            return Ok(userToken);
        }

        private string GenerateToken(UserEntity userDto)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credential = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDto.UserName),
                new Claim("CustomerGuid", userDto.Id.ToString()),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var token =
                new JwtSecurityToken(
                    _configuration["JWT:Issuer"],
                    _configuration["JWT:Audience"],
                    claims,
                    expires: DateTime.Now.AddMinutes(15),
                    signingCredentials: credential
                );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        [HttpPost]
        [Route("/register")]
        public IActionResult RegisterUser (UserRegistrationDTO RegistrationDTO)
        {
            UserEntity NewUserEntity = new UserEntity()
            {
                UserName = RegistrationDTO.UserName,
                Password = RegistrationDTO.Password,
                SubscriptionEndDate = DateTimeOffset.Now,
                CreatedAt = DateTimeOffset.Now,
            };
            
            _userRepository.AddUser(NewUserEntity);

            return Ok(NewUserEntity);
        }
    }
}
