using MediatR;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;

namespace SocialNetwork.Handlers
{
    public class AcceptHandler : IRequestHandler<AcceptCommand, bool>
    {
        private readonly IFriendRepository _friendRepository;

        public AcceptHandler(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        public async Task<bool> Handle(AcceptCommand request, CancellationToken cancellationToken)
        {
            await _friendRepository.Refuse(request.SenderId, request.ReceiverId);
            return true;
        }
    }
}
