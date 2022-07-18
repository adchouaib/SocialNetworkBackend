using MediatR;

namespace SocialNetwork.Commands
{
    public class LikePostCommand : IRequest<bool>
    {
        public Guid postId { get; set; }
        public Guid userId { get; set; }

        public LikePostCommand(Guid postid, Guid userid)
        {
            postId = postid;
            userId = userid;
        }
    }
}
