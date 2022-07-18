using MediatR;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;

namespace SocialNetwork.Handlers
{
    public class RejectHandler : IRequestHandler<RejectCommand, bool>
    {
        private readonly IFriendRepository _friendRepository;

        public RejectHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<bool> Handle(RejectCommand request, CancellationToken cancellationToken)
        {
            await _friendRepository.Unfriend(request.SenderId, request.ReceiverId);
            return true;
        }
    }
}
