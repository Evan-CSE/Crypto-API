using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebsite.Model.Entities
{
    public class PostEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        [ForeignKey("UserEntity")]
        public Guid UserId { get; set; }
    }
}
