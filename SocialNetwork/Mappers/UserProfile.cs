using AutoMapper;
using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Mappers
{
    public class UserProfile:Profile
    {
        public UserProfile()
        {
            CreateMap<User, UserDTO>();
        }
    }

    public class UserRegistrationProfile : Profile
    {
        public UserRegistrationProfile()
        {
            CreateMap<User, UserRegisterDTO>();
        }
    }

    public class UserLoginProfile : Profile
    {
        public UserLoginProfile()
        {
            CreateMap<User, UserLoginDTO>();
        }
    }
}
