using SocialNetwork.DTOs;

namespace SocialNetwork.Data.IRepositories
{
    public interface IFriendRepository
    {
        Task Invite(Guid senderId, Guid receiverId);
        Task Accept(Guid senderId, Guid receiverId);
        Task Refuse(Guid senderId, Guid receiverId);
        Task Unfriend(Guid senderId, Guid receiverId);
        Task<List<UserDTO>> GetUserFriends(Guid userId);
        Task<List<PostDTO>> GetFriendsPosts(Guid userId);
        Task<List<UserDTO>> GetNonUserFriends(Guid userId);
        Task<List<UserDTO>> GetUserInvitations(Guid userId);
    }
}
