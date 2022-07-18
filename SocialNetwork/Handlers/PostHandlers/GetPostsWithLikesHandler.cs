using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;
using System.Security.Claims;

namespace SocialNetwork.Handlers
{
    public class GetPostsWithLikesHandler : IRequestHandler<GetPostsWithLikes, List<PostDTO>>
    {

        private readonly IPostRepository _postRepository;

        public GetPostsWithLikesHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostDTO>> Handle(GetPostsWithLikes request, CancellationToken cancellationToken)
        {
            List<PostDTO> posts = await _postRepository.GetPosts(request.UserId);
            return posts;
        }
    }
}
