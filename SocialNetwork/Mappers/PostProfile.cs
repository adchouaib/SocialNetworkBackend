using AutoMapper;
using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Mappers
{
    public class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateProjection<Post, PostDTO>()
                .ForMember(dest =>
                    dest.AuthorName,
                    opt => opt.MapFrom(src => src.User.FullName))
                .ForMember(dest =>
                    dest.AuthorId,
                    opt => opt.MapFrom(src => src.User.Id))
                .ForMember(dest =>
                    dest.AuthorAvatar,
                    opt => opt.MapFrom(src => src.User.Avatar));
                
        }
    }

    public class AddPostProfile : Profile
    {
        public AddPostProfile()
        {
            CreateMap<Post, PostDTO>();
        }
    }
}
