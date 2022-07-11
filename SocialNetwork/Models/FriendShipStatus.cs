using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class FriendShipStatus
    {
        [ForeignKey("Friends")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Status StatusName { get; set; }
        
        public virtual Friends Friends { get; set; }

    }
}
