using BlogWebsite.Model;
using BlogWebsite.Model.Entities;

namespace BlogWebsite.Repository
{
    public interface IUserRepository
    {
        public List<UserEntity> GetAllUser();
        public UserEntity? GetUserById(Guid id);
        public UserEntity AddUser(UserEntity user);
        public UserEntity? GetUserByUserNameAndPassword(UserLoginDto loginDto);
        public UserEntity? MaybeGetUserByToken(string token);
    }
}
