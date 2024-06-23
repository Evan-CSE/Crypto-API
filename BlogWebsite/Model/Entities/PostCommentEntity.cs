using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebsite.Model.Entities
{
    public class PostCommentEntity
    {

        public int Id { get; set; }
        public string Content { get; set; }
        public DateTimeOffset CreatedDate { get; set; }

        [ForeignKey("UserEntity")]
        public Guid UserId { get; set; }

        [ForeignKey("PostEntity")]
        public int PostId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
