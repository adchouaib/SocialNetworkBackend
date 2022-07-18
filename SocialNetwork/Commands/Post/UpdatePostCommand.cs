using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Commands
{
    public class UpdatePostCommand : IRequest<PostDTO>
    {
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid PostId { get; set; } = Guid.Empty;
    }
}
