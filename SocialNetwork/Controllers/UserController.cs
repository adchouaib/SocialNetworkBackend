using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Queries;

namespace SocialNetwork.Controllers
{
    [Route("/api/User")]
    [ApiController]
    public class UserController : Controller
    {
        
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var Query = new GetUsers();
            var Result = await _mediator.Send(Query);
            return Ok(Result);
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDTO>> Get(Guid id)
        {
            var Query = new GetUserById(id);
            var Result = await _mediator.Send(Query);
            return Result != null ? Ok(Result) : NotFound("User Not Found");
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDTO>> Put(UserDTO user)
        {
            UpdateUserCommand command = new UpdateUserCommand(user);
            var result = await _mediator.Send(command);
            return result == null ? BadRequest("could not be updated") : Ok(result);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(Guid id)
        {
            DeleteUserCommand command = new DeleteUserCommand(id);
            await _mediator.Send(command);
            return Ok("user Deleted");
        }
    }
}
