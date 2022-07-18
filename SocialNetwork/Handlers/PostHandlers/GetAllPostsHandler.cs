using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;

namespace SocialNetwork.Handlers
{
    public class GetAllPostsHandler : IRequestHandler<GetAllPosts, List<PostDTO>>
    {
        private readonly IPostRepository _postRepository;

        public GetAllPostsHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<List<PostDTO>> Handle(GetAllPosts request, CancellationToken cancellationToken)
        {
            List<PostDTO> posts = await _postRepository.GetPosts();
            return posts;
        }
    }
}
