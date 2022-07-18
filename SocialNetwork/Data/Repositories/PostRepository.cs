using AutoMapper;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Data.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PostRepository(DataContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PostDTO> addPost(Post post)
        {
            using (var context = _context)
            {
                await context.Posts.AddAsync(post);
                await context.SaveChangesAsync();
                var newPost = _mapper.Map<PostDTO>(post);
                return await Task.FromResult(newPost);
            }
        }

        public async Task deletePost(Guid id)
        {
            using (var context = _context)
            {
                var post = await context.Posts.FirstOrDefaultAsync(p => p.Id == id);
                if (post == null)
                    return;

                context.Entry(post).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<DTOs.PostDTO> getPostById(Guid id)
        {
            using (var context = _context)
            {
                Post? post = await context.Posts
                                          .Include(p => p.User)
                                          .FirstOrDefaultAsync(p => p.Id == id);

                if(post != null)
                {
                    var postDTO = _mapper.Map<PostDTO>(post);
                    return await Task.FromResult(postDTO);
                }
                return null;
            }
        }

        public async Task<List<PostDTO>> GetPosts()
        {
            using (var context = _context)
            {
                List<PostDTO> posts = await context.Posts
                                                .Include(p => p.User)
                                                .OrderByDescending(p => p.CreatedDate)
                                                .Select(p => _mapper.Map<PostDTO>(p) )
                                                .ToListAsync();

                return await Task.FromResult(posts);
            }
        }

        public async Task<List<PostDTO>> GetPosts(Guid UserId)
        {
            using (var context = _context)
            {
                List<PostDTO> posts = await context.Posts
                                                .Include(p => p.User)
                                                .OrderByDescending(p => p.CreatedDate)
                                                .Select(p =>_mapper.Map<PostDTO>(p))  
                                                .ToListAsync();

                posts.ForEach(p => p.IsLikedByUser = context.Likes.FirstOrDefault(l => l.UserId == UserId && l.PostId == p.Id) != null);

                return await Task.FromResult(posts);
            }
        }

        public async Task<List<Post>> GetUsersPosts(Guid UserId)
        {
            using (var context = _context)
            {
                List<Post> userPosts = await context.Posts
                        .Where(p => p.AuthorId == UserId)
                        .ToListAsync();

                return await Task.FromResult(userPosts);
            }
        }

        public async Task<PostDTO> updatePost(PostDTO postdto , Guid PostId)
        {
            using (var context = _context)
            {
                Post? oldPost = await context.Posts.FirstOrDefaultAsync(p => p.Id == PostId);
                if (oldPost == null)
                    return null;
                    
                oldPost.Description = postdto.Description;
                oldPost.Title = postdto.Title;
                oldPost.Content = postdto.Content;
                await context.SaveChangesAsync();

                PostDTO newPost = _mapper.Map<PostDTO>(oldPost);

                return await Task.FromResult(newPost);
            }
        }

        public async Task LikePost(Guid UserId , Guid PostId)
        {
            using(var context = _context)
            {
                var like = await context.Likes.FirstOrDefaultAsync(l => l.UserId == UserId && l.PostId == PostId);
                if(like != null)
                {
                    context.Entry(like).State = EntityState.Deleted;
                    await context.SaveChangesAsync();
                }
                else
                {
                    User user = await context.Users.FirstAsync(u => u.Id == UserId);
                    Post post = await context.Posts.FirstAsync(p => p.Id == PostId);

                    Likes likes = new Likes()
                    {
                        User = user,
                        Post = post
                    };

                    await context.Likes.AddAsync(likes);
                    await context.SaveChangesAsync();
                }

            }
        }

        public async Task<bool> IsPostLiked(Guid postId, Guid UserId)
        {
            using (var context = _context)
            {
                bool Like = await context.Likes.FirstOrDefaultAsync(l => l.UserId == UserId && l.PostId == postId) != null;
                return Like;
            }
        }

        
    }
}
