using MediatR;

namespace SocialNetwork.Commands
{
    public class InviteCommand : IRequest<bool>
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }

        public InviteCommand(Guid senderId , Guid receiverId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}
