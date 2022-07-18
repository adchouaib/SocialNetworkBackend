using AutoMapper;
using SocialNetwork.Helpers.Mapping;
using SocialNetwork.Models;

namespace SocialNetwork.DTOs
{
    public class PostDTO : IMapTo<Post> , IMapFrom<Post>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string Content { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty ;
        public string AuthorName { get; set; } = String.Empty;
        public string AuthorAvatar { get; set; } = String.Empty;
        public bool IsLikedByUser { get; set; } = false;

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Post, PostDTO>()
                .ForMember(d => d.AuthorName, opt => opt.MapFrom(s => s.User.FullName))
                .ForMember(d => d.AuthorAvatar, opt => opt.MapFrom(s => s.User.Avatar))
                .ForMember(d => d.AuthorId, opt => opt.MapFrom(s => s.User.Id));

            profile.CreateMap<PostDTO, Post>();
        }
    }

    public class AddPostDto : IMapTo<Post>, IMapFrom<Post>
    {
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<Post, AddPostDto>();
            
            profile.CreateMap<AddPostDto, Post>();
        }
    }

}
