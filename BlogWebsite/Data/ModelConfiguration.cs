using BlogWebsite.Model.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlogWebsite.Data
{
    public class ModelConfiguration
    {
        public ModelConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasIndex(user => user.UserName)
                .IsUnique(true);
        }
    }
}
