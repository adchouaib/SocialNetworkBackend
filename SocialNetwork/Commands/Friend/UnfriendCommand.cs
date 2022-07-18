using MediatR;

namespace SocialNetwork.Commands
{
    public class UnfriendCommand : IRequest<bool>
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }

        public UnfriendCommand(Guid senderId, Guid receiverId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}
