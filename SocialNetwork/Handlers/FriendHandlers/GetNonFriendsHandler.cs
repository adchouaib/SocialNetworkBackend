using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;

namespace SocialNetwork.Handlers
{
    public class GetNonFriendsHandler : IRequestHandler<GetNonFriends, List<UserDTO>>
    {
        private readonly IFriendRepository _friendRepository;

        public GetNonFriendsHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<List<UserDTO>> Handle(GetNonFriends request, CancellationToken cancellationToken)
        {
            List<UserDTO> userDTOs = await _friendRepository.GetNonUserFriends(request.UserId);
            return userDTOs;
        }
    
    }
}
