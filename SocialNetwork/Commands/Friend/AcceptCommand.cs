using MediatR;

namespace SocialNetwork.Commands
{
    public class AcceptCommand : IRequest<bool>
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }

        public AcceptCommand(Guid senderId , Guid receiverId)
        {
            SenderId = senderId;
            ReceiverId = receiverId;
        }
    }
}
