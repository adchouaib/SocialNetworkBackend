using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Queries
{
    public class GetPostById : IRequest<PostDTO>
    {
        public Guid PostId { get; }

        public GetPostById(Guid postId)
        {
            PostId = postId;
        }
    }
}
