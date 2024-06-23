using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebsite.Model.Entities
{
    public class PostLikeEntity
    {
        public int Id { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        [ForeignKey("UserEntity")]
        public Guid UserId { get; set; }

        [ForeignKey("PostEntity")]
        public int PostId { get; set; }
    }
}
