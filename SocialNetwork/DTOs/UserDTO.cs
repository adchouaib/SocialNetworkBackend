using AutoMapper;
using SocialNetwork.Helpers.Mapping;
using SocialNetwork.Models;

namespace SocialNetwork.DTOs
{
    public class UserDTO : IMapFrom<User> , IMapTo<User>
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = String.Empty;
        public string Avatar { get; set; } = String.Empty;
        public string Work { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }

        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDTO>();

            profile.CreateMap<UserDTO, User>();
        }
    }

    public class UserRegisterDTO : IMapFrom<User>, IMapTo<User>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = String.Empty;
        public string Avatar { get; set; } = String.Empty;
        public string Work { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDTO>();

            profile.CreateMap<UserDTO, User>();
        }
    }

    public class UserLoginDTO : IMapFrom<User>, IMapTo<User>
    {
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;
        public virtual void Mapping(Profile profile)
        {
            profile.CreateMap<User, UserDTO>();

            profile.CreateMap<UserDTO, User>();
        }
    }
}
