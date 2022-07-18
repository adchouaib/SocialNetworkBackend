using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Queries
{
    public class GetPostsWithLikes : IRequest<List<PostDTO>>
    {
        public Guid UserId { get; }

        public GetPostsWithLikes(Guid userId)
        {
            UserId = userId;
        }
    }
}
