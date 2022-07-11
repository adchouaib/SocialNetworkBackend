using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class Post
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } 

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Description { get; set; } = string.Empty ;

        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;

        [ForeignKey("AuthorId") , Required]
        public User User { get; set; }

        public Guid AuthorId { get; set; }

    }
}
