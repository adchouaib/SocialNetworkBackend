using MediatR;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;

namespace SocialNetwork.Handlers
{
    public class GetUsersHandler : IRequestHandler<GetUsers, List<UserDTO>>
    {
        private readonly IUserRepository _userRepository;

        public GetUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDTO>> Handle(GetUsers request, CancellationToken cancellationToken)
        {
            List<UserDTO> users = await _userRepository.getUsers();
            return users;
        }
    }
}
