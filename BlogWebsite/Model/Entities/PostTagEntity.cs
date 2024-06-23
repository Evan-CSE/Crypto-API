using System.ComponentModel.DataAnnotations.Schema;

namespace BlogWebsite.Model.Entities
{
    public class PostTagEntity
    {
        public int Id { get; set; }
        [ForeignKey("PostEntity")]
        public int PostId { get; set; }
        [ForeignKey("TagEntity")]
        public int TagId { get; set; } 
    }
}
