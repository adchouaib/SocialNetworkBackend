using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Commands
{
    public class AddPostCommand : IRequest<PostDTO>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }

        public AddPostCommand(string title, string description, string content, Guid authorId)
        {
            Title = title;
            Description = description;
            Content = content;
            AuthorId = authorId;
        }
    }
}
