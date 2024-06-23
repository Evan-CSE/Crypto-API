using BlogWebsite.Data;
using BlogWebsite.Model.Entities;

namespace BlogWebsite.Repository
{
    public class PostRepository(DbContextEntity dbContext) : IPostRepository
    {
        private readonly DbContextEntity dbContext = dbContext;

        public List<PostEntity> GetAllPostForUser(Guid userId)
        {
            var maybeAllPost = dbContext.Posts.Where (p => p.UserId == userId).ToList();

            return maybeAllPost;
        }

        public List<PostEntity> GetAllPosts()
        {
            return dbContext.Posts.ToList();
        }

        public PostEntity? GetPostById(int id)
        {
            return dbContext.Posts.Where(post => post.Id == id).FirstOrDefault();
        }

        public PostEntity? RemovePost(int id)
        {
            var maybePost = GetPostById(id);

            if (maybePost == null)
            {
                return null;
            }

            dbContext.Posts.Remove(maybePost);
            dbContext.SaveChanges();

            return maybePost;
        }

        public PostEntity SavePost(PostEntity post)
        {
            dbContext.Posts.Add(post);
            dbContext.SaveChanges();
            return post;
        }
    }
}
