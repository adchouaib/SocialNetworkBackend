using MediatR;

namespace SocialNetwork.Queries
{
    public class IsLicked : IRequest<bool>
    {
        public Guid UserId { get; }
        public Guid PostId { get; }

        public IsLicked(Guid userId , Guid postId)
        {
            UserId = userId;
            PostId = postId;
        }
    }
}
