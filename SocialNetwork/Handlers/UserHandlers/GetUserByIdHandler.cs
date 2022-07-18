using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;

namespace SocialNetwork.Handlers
{
    public class GetUserByIdHandler : IRequestHandler<GetUserById, UserDTO>
    {

        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDTO> Handle(GetUserById request, CancellationToken cancellationToken)
        {
            var User = await _userRepository.getUserById(request.UserId);
            return User;
        }
    }
}
