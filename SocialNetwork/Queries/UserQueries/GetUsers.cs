using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Queries
{
    public class GetUsers : IRequest<List<UserDTO>>
    {

    }
}
