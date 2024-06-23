using BlogWebsite.Model.Entities;

namespace BlogWebsite.Repository
{
    public interface IPostRepository
    {
        public List<PostEntity> GetAllPosts();
        public PostEntity? GetPostById(int id);
        public List<PostEntity> GetAllPostForUser(Guid userId);

        public PostEntity SavePost(PostEntity post);

        public PostEntity? RemovePost(int id);
    }
}
