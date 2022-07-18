using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Helpers.Validators;
using SocialNetwork.Queries;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    [Route("/api/Friend")]
    [ApiController]
    public class FriendController : Controller
    {
        private readonly IMediator _mediator;

        public FriendController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("Friends")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            try
            {
                var Query = new GetFriends(new Guid(User.FindFirstValue("id")));
                var Result = await _mediator.Send(Query);
                return Ok(Result);
            }
            catch(Exception ex)
            {
                return StatusCode(500, "Internal Server Error");
            }
        }

        [HttpGet("NonFriends")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserDTO>>> GetNonFriends()
        {
            var Query = new GetNonFriends(new Guid(User.FindFirstValue("id")));
            var Result = await _mediator.Send(Query);
            return Ok(Result);
        }

        [HttpGet("FriendsPosts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserDTO>>> GetFriendsPosts()
        {
            var Query = new GetFriendsPosts(new Guid(User.FindFirstValue("id")));
            var Result = await _mediator.Send(Query);
            return Ok(Result);
        }

        [HttpGet("Invitations")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserDTO>>> GetInvitations()
        {
            var Query = new GetInvitations(new Guid(User.FindFirstValue("id")));
            var Result = _mediator.Send(Query);
            return Ok(Result);
        }

        [HttpPost("Invite")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Invite(Guid receiverId)
        {
            InviteCommand command = new InviteCommand(new Guid(User.FindFirstValue("id")) , receiverId);
            await _mediator.Send(command);
            return Ok("user invited");
        }

        [HttpPut("Accept")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Accept(Guid receiverId)
        {
            AcceptCommand command = new AcceptCommand(new Guid(User.FindFirstValue("id")), receiverId);
            var Result = await _mediator.Send(command);
            return Ok("user accepted");
        }

        [HttpPut("Reject")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Refuse(Guid receiverId)
        {
            AcceptCommand command = new AcceptCommand(new Guid(User.FindFirstValue("id")), receiverId);
            var Result = await _mediator.Send(command);
            return Ok("user invited");
        }

        [HttpDelete("Unfriend")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Unfriend(Guid receiverId)
        {
            UnfriendCommand command = new UnfriendCommand(new Guid(User.FindFirstValue("id")), receiverId);
            await _mediator.Send(command);
            return Ok("user invited");
        }

    }
}
