using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {

        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;

        public PostController(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostDTO>>> Get()
        {
            List<PostDTO> posts = await _postRepository.GetPosts();
            return Ok(posts);
        }

        [HttpGet("WithLikes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostDTO>>> GetWithLike()
        {
            Guid userID = new Guid(User.FindFirstValue("id"));
            List<PostDTO> posts = await _postRepository.GetPosts(userID);
            return Ok(posts);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<Post>> Post(AddPostDto post)
        {
            Post newPost = new Post()
            {
                AuthorId = post.AuthorId,
                Content = post.Content,
                Description = post.Description,
                Title = post.Title,
            };

            await _postRepository.addPost(newPost);
            return Ok(newPost);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostDTO>> Get(Guid id)
        {
            var Post = _postRepository.getPostById(id);
            if (Post == null)
            {
                return NotFound("Post not found");
            }
            else
            {
                return Ok(Post);
            }
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostDTO>> Put(PostDTO postDto, Guid PostId)
        {
            var Post = await _postRepository.updatePost(postDto, PostId);
            if (Post == null)
                return NotFound("Post Not Found");
            return Ok(Post);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task Delete(Guid id)
        {
            await _postRepository.deletePost(id);
        }

        [HttpPost("Like")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task Post(Guid postId)
        {
            Guid userID = new Guid(User.FindFirstValue("id"));
            await _postRepository.LikePost(userID, postId);
        }

        [HttpGet("isLiked")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> IsLiked(Guid postId)
        {
            Guid userID = new Guid(User.FindFirstValue("id"));
            bool isLiked = await _postRepository.IsPostLiked(postId, userID);
            return Ok(isLiked);
        }
    }

}
