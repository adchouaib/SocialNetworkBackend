using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class Friends
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid User1Id { get; set; }

        [ForeignKey("User1Id")]
        public User User1 { get; set; }

        public Guid User2Id { get; set; }

        [ForeignKey("User2Id")]
        public User User2 { get; set; }

        public virtual FriendShipStatus Status { get; set; }
    }
}
