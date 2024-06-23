using BlogWebsite.Data;
using BlogWebsite.Model.Entities;
using BlogWebsite.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace BlogWebsite.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(DbContextEntity dbContext) : ControllerBase
    {
        private readonly IUserRepository _userRepository = new UserRepository(dbContext);

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            var maybeUserToken = Request.Headers["Authorization"].ToString();

            if (string.IsNullOrEmpty(maybeUserToken) || !maybeUserToken.StartsWith("Bearer "))
            {
                return Unauthorized();
            }

            // get customer from db

            var maybeUser = _userRepository.MaybeGetUserByToken(maybeUserToken);

            if (maybeUser == null) 
            {
                return NotFound();
            }

            return Ok(maybeUser);
        }

        private UserEntity? GetCurrentUser(Guid id) 
        {
            return _userRepository.GetUserById(id);
        }
    }
}
