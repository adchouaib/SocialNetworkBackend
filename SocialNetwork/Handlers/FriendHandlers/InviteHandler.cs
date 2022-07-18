using MediatR;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;

namespace SocialNetwork.Handlers
{
    public class InviteHandler : IRequestHandler<InviteCommand, bool>
    {

        private readonly IFriendRepository _friendRepository;

        public InviteHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<bool> Handle(InviteCommand request, CancellationToken cancellationToken)
        {
            await _friendRepository.Invite(request.SenderId, request.ReceiverId);
            return true;
        }
    }
}
