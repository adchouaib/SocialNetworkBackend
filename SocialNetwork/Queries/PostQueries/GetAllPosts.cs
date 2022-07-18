using MediatR;
using SocialNetwork.DTOs;

namespace SocialNetwork.Queries
{
    public class GetAllPosts : IRequest<List<PostDTO>>
    {

    }
}
