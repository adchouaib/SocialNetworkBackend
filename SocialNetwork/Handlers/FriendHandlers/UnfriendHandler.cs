using MediatR;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;

namespace SocialNetwork.Handlers
{
    public class UnfriendHandler : IRequestHandler<UnfriendCommand, bool>
    {
        private readonly IFriendRepository _friendRepository;

        public UnfriendHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<bool> Handle(UnfriendCommand request, CancellationToken cancellationToken)
        {
            await _friendRepository.Unfriend(request.SenderId, request.ReceiverId);
            return true;
        }
    }
}
