using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;

namespace SocialNetwork.Handlers
{
    public class GetFriendsPostsHandler : IRequestHandler<GetFriendsPosts, List<PostDTO>>
    {
        private readonly IFriendRepository _friendRepository;

        public GetFriendsPostsHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<List<PostDTO>> Handle(GetFriendsPosts request, CancellationToken cancellationToken)
        {
            List<PostDTO> posts = await _friendRepository.GetFriendsPosts(request.UserId);
            return posts;
        }
    
    }
}
