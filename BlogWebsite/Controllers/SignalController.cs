using BlogWebsite.Data;
using BlogWebsite.Model.Entities;
using BlogWebsite.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebsite.Controllers
{
    [Route("/api/[Controller]")]
    [ApiController]
    public class SignalController(DbContextEntity dbContext, IConfiguration configuration) : ControllerBase
    {
        private readonly IConfiguration _configuration = configuration;
        private readonly IUserRepository _userRepository = new UserRepository(dbContext);
        private readonly IPostRepository _postRepository = new PostRepository(dbContext);

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Get()
        {
            return Ok(_postRepository.GetAllPosts());
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Post(string postText)
        {
            var maybeToken = Request.Headers.Authorization.ToString();
            var res = maybeToken.StartsWith("Bearer ");
            if (string.IsNullOrEmpty(maybeToken) || !maybeToken.StartsWith("Bearer "))
            {
                return Unauthorized();
            }

            // verify if the user really has the role
            var maybeUser = _userRepository.MaybeGetUserByToken(maybeToken);

            // add a check for the role
            if (maybeUser == null) 
            {
                return Unauthorized();
            }

            var newPostEntity = new PostEntity()
            {
                Title = postText,
                Content = postText,
                CreatedDate = DateTime.Now,
                UserId = maybeUser.Id
            };

            _postRepository.SavePost(newPostEntity);

            return Ok(newPostEntity);
        }

    }
}
