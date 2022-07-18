using MediatR;

namespace SocialNetwork.Commands
{
    public class DeleteUserCommand : IRequest<bool>
    {

        public Guid UserID { get; set; }

        public DeleteUserCommand(Guid userId)
        {
            UserID = userId;
        }
    }
}
