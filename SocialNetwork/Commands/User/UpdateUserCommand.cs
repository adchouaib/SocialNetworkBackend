using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Commands
{
    public class UpdateUserCommand : IRequest<UserDTO>
    {
        public UserDTO User { get; set; }

        public UpdateUserCommand(UserDTO user)
        {
            User = user;
        }
    }
}
