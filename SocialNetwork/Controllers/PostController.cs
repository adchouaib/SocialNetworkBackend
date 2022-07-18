using AutoMapper;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Helpers.Validators;
using SocialNetwork.Models;
using SocialNetwork.Queries;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : Controller
    {

        private readonly IMediator _mediator;

        public PostController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostDTO>>> Get()
        {
            var Query = new GetAllPosts();
            var Result = await _mediator.Send(Query);
            return Ok(Result);
        }

        [HttpGet("WithLikes")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<PostDTO>>> GetWithLike()
        {
            var Query = new GetPostsWithLikes(new Guid(User.FindFirstValue("id")));
            var Result = await _mediator.Send(Query);
            return Ok(Result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostDTO>> Post(AddPostDto postDto)
        {
            try
            {
                AddPostCommand command = new AddPostCommand(postDto.Title, postDto.Description, postDto.Content, new Guid(User.FindFirstValue("id")));
                var Result = await _mediator.Send(command);
                return Ok("Created");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            } 
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostDTO>> Get(Guid id)
        {
            var Query = new GetPostById(id);
            var Result = await _mediator.Send(Query);
            return Result != null ? Ok(Result) : NotFound("Post not found");
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<PostDTO>> Put([FromBody] UpdatePostCommand command)
        {
            try
            {
                var Result = await _mediator.Send(command);
                return Result != null ? Ok(Result) : NotFound("Post Not Found");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task Delete(Guid id)
        {
            DeletePostCommand command = new DeletePostCommand(id);
            var Result = _mediator.Send(command);
        }

        [HttpPost("Like")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task Post(Guid postId)
        {
            Guid userID = new Guid(User.FindFirstValue("id"));
            LikePostCommand command = new LikePostCommand(postId, userID);
            await _mediator.Send(command);
        }

        [HttpGet("isLiked")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<bool>> IsLiked(Guid postId)
        {
            var Query = new IsLicked(new Guid(User.FindFirstValue("id")), postId);
            var Result = _mediator.Send(Query);
            return Ok(Result);
        }
    }

}
