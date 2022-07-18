using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.Queries;

namespace SocialNetwork.Handlers
{
    public class IsLickedHandler : IRequestHandler<IsLicked, bool>
    {
        private readonly IPostRepository _postRepository;

        public IsLickedHandler(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> Handle(IsLicked request, CancellationToken cancellationToken)
        {
            bool isLiked = await _postRepository.IsPostLiked(request.PostId, request.UserId);
            return isLiked;
        }
    }
}
