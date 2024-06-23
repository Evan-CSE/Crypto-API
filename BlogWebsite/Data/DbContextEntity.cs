using BlogWebsite.Model.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Reflection.Metadata;

namespace BlogWebsite.Data
{
    public class DbContextEntity : DbContext
    {
        public DbContextEntity(DbContextOptions options) : base(options)
        {
        }

        #region required
        protected override void OnModelCreating(ModelBuilder modelbuilder)
        {
            var modelconfiguration = new ModelConfiguration(modelbuilder);
        }
        #endregion

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<PostEntity> Posts { get; set; }
        public DbSet<PostLikeEntity> PostLike { get; set; }
        public DbSet<PostCommentEntity> PostComment { get; set; }
        public DbSet<PostTagEntity> PostTag { get; set; }
        public DbSet<TagEntity> Tag { get; set; }

    }
}
