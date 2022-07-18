using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;

namespace SocialNetwork.Handlers
{
    public class GetPostByIdHandler : IRequestHandler<GetPostById, PostDTO>
    {
        private readonly IPostRepository _postRepository;

        public GetPostByIdHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<PostDTO> Handle(GetPostById request, CancellationToken cancellationToken)
        {
            var Post = await _postRepository.getPostById(request.PostId);
            return Post;
        }
    }
}
