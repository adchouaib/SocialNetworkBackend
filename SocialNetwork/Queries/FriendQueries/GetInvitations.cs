using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Queries
{
    public class GetInvitations : IRequest<List<UserDTO>>
    {
        public Guid UserId { get; }

        public GetInvitations(Guid userId)
        {
            UserId = userId;
        }
    }
}
