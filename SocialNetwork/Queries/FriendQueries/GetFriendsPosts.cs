using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Queries
{
    public class GetFriendsPosts : IRequest<List<PostDTO>>
    {
        public Guid UserId { get; }

        public GetFriendsPosts(Guid userId)
        {
            UserId = userId;
        }
    }
}
