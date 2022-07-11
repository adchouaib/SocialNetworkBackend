using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class Likes
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        public Guid PostId { get; set; }

        [ForeignKey("PostId")]
        public Post Post { get; set; }
    }
}
