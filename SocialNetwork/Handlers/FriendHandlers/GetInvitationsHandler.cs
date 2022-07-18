using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;

namespace SocialNetwork.Handlers
{
    public class GetInvitationsHandler : IRequestHandler<GetInvitations, List<UserDTO>>
    {
        private readonly IFriendRepository _friendRepository;

        public GetInvitationsHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<List<UserDTO>> Handle(GetInvitations request, CancellationToken cancellationToken)
        {
            List<UserDTO> invitations = await _friendRepository.GetUserInvitations(request.UserId);
            return invitations;
        }

    }
}
