using MediatR;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;

namespace SocialNetwork.Handlers
{
    public class DeleteUserHandler : IRequestHandler<DeleteUserCommand, bool>
    {

        private readonly IUserRepository _userRepository;

        public DeleteUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.deleteUser(request.UserID);
            return true;
        }
    }
}
