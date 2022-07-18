using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Queries
{
    public class GetFriends : IRequest<List<UserDTO>>
    {
        public Guid UserId { get; }

        public GetFriends(Guid userId)
        {
            UserId = userId;
        }
    }
}
