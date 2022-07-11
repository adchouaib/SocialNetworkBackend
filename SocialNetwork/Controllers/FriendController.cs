using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using System.Security.Claims;

namespace SocialNetwork.Controllers
{
    [Route("/api/Friend")]
    [ApiController]
    public class FriendController : Controller
    {
        private readonly IFriendRepository _friendRepository;

        public FriendController(IFriendRepository friendRepository)
        {
            _friendRepository = friendRepository;
        }

        [HttpGet("Friends")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            try
            {
                Guid UserId = new Guid(User.FindFirstValue("id"));
                List<UserDTO> userDTOs = await _friendRepository.GetUserFriends(UserId);
                return Ok(userDTOs);
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
            Guid UserId = new Guid(User.FindFirstValue("id"));
            List<UserDTO> userDTOs = await _friendRepository.GetNonUserFriends(UserId);
            return Ok(userDTOs);
        }

        [HttpGet("FriendsPosts")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserDTO>>> GetFriendsPosts()
        {
            Guid UserId = new Guid(User.FindFirstValue("id"));
            List<PostDTO> posts = await _friendRepository.GetFriendsPosts(UserId);
            return Ok(posts);
        }

        [HttpGet("Invitations")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserDTO>>> GetInvitations()
        {
            Guid UserId = new Guid(User.FindFirstValue("id"));
            List<UserDTO> invitations = await _friendRepository.GetUserInvitations(UserId);
            return Ok(invitations);
        }

        [HttpPost("Invite")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Invite(Guid receiverId)
        {
            Guid senderId = new Guid(User.FindFirstValue("id"));
            await _friendRepository.Invite(senderId, receiverId);
            return Ok("user invited");
        }

        [HttpPut("Accept")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Accept(Guid receiverId)
        {
            Guid senderId = new Guid(User.FindFirstValue("id"));
            await _friendRepository.Accept(senderId, receiverId);
            return Ok("user invited");
        }

        [HttpPut("Reject")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Refuse(Guid receiverId)
        {
            Guid senderId = new Guid(User.FindFirstValue("id"));
            await _friendRepository.Refuse(senderId, receiverId);
            return Ok("user invited");
        }

        [HttpDelete("Unfriend")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Unfriend(Guid receiverId)
        {
            Guid senderId = new Guid(User.FindFirstValue("id"));
            await _friendRepository.Unfriend(senderId, receiverId);
            return Ok("user invited");
        }

    }
}
