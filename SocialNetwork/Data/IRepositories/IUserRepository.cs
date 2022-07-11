using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Data.IRepositories
{
    public interface IUserRepository
    {
        Task<List<UserDTO>> getUsers();
        Task<UserDTO> getUserById(Guid id);
        Task<User> getUserByEmail(string email);
        Task<UserDTO> addUser(User user);
        Task<UserDTO> updateUser(UserDTO user);
        Task deleteUser(Guid id); 
        Task<bool> isEmailExisting(string email);


    }
}
