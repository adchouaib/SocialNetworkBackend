using MediatR;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;

namespace SocialNetwork.Handlers
{
    public class LikePostHandler : IRequestHandler<LikePostCommand, bool>
    {

        private readonly IPostRepository _postRepository;

        public LikePostHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> Handle(LikePostCommand request, CancellationToken cancellationToken)
        {
            await _postRepository.LikePost(request.userId, request.postId);
            return true;
        }
    }
}
