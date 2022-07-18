using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Queries
{
    public class GetUserById : IRequest<UserDTO>
    {
        public Guid UserId { get; }

        public GetUserById(Guid userId)
        {
            UserId = userId;
        }
    }
}
