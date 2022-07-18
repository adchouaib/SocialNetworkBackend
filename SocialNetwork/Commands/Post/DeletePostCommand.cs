using MediatR;

namespace SocialNetwork.Commands
{
    public class DeletePostCommand : IRequest<bool>
    {
        public Guid postId { get; set; }

        public DeletePostCommand(Guid postid)
        {
            postId = postid;
        }
    }
}
