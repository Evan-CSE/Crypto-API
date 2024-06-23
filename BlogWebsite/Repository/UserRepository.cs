using BlogWebsite.Data;
using BlogWebsite.Model;
using BlogWebsite.Model.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace BlogWebsite.Repository
{
    public class UserRepository(DbContextEntity dbContext) : IUserRepository
    {
        private readonly DbContextEntity _dbContext = dbContext;

        public UserEntity? GetUserById(Guid id)
        {
            return _dbContext.Users.SingleOrDefault(user => user.Id == id);
        }

        public List<UserEntity> GetAllUser()
        {
            return _dbContext.Users.ToList();
        }

        public UserEntity AddUser(UserEntity user) 
        {
            this._dbContext.Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public UserEntity? GetUserByUserNameAndPassword(UserLoginDto loginDto) 
        {
            var maybeUser =
                _dbContext.Users.FirstOrDefault(
                        user => 
                            user.UserName == loginDto.UserName 
                            && user.Password == loginDto.Password
                );

            return maybeUser;
        }

        public UserEntity? MaybeGetUserByToken(string token)
        {
            if (string.IsNullOrEmpty(token) || !token.StartsWith("Bearer "))
            {
                return null;
            }

            token = token.Substring("Bearer ".Length).Trim();

            if (string.IsNullOrEmpty(token))
            {
                return null;
            }

            // Decode the token and extract the customerGuid
            var tokenHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken;

            try
            {
                jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
            }
            catch
            {
                return null;
            }

            if (jwtToken == null)
            {
                return null;
            }

            var customerGuidClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == "CustomerGuid")?.Value;

            if (string.IsNullOrEmpty(customerGuidClaim) || !Guid.TryParse(customerGuidClaim, out var customerGuid))
            {
                return null;
            }

            var maybeUser = _dbContext.Users.FirstOrDefault(user => user.Id == customerGuid);

            return maybeUser;
        }
    }
}
