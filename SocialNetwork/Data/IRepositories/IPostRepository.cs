using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Data.IRepositories
{
    public interface IPostRepository
    {
        Task<List<PostDTO>> GetPosts();
        Task<PostDTO> getPostById(Guid id);
        Task<List<Post>> GetUsersPosts(Guid id);
        Task<PostDTO> addPost(Post post);
        Task<PostDTO> updatePost(PostDTO postdto, Guid PostId);
        Task deletePost(Guid id);
        Task LikePost(Guid postId , Guid UserId);
        Task<bool> IsPostLiked(Guid postId, Guid UserId);
        Task<List<PostDTO>> GetPosts(Guid UserId);
    }
}
