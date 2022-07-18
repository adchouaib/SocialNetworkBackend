using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;

namespace SocialNetwork.Handlers
{
    public class GetFriendsHandler : IRequestHandler<GetFriends, List<UserDTO>>
    {
        private readonly IFriendRepository _friendRepository;

        public GetFriendsHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<List<UserDTO>> Handle(GetFriends request, CancellationToken cancellationToken)
        {
            List<UserDTO> userDTOs = await _friendRepository.GetUserFriends(request.UserId);
            return userDTOs;
        }
    }
}
