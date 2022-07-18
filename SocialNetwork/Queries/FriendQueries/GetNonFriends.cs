using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Queries
{
    public class GetNonFriends : IRequest<List<UserDTO>>
    {
        public Guid UserId { get; }

        public GetNonFriends(Guid userId)
        {
            UserId = userId;
        }
    }
}
