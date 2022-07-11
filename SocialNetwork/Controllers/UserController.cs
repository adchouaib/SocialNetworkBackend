using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;

namespace SocialNetwork.Controllers
{
    [Route("/api/User")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            return Ok(await _userRepository.getUsers());
        }

        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDTO>> Get(Guid id)
        {
            var User = await _userRepository.getUserById(id);
            if (User == null)
                return BadRequest(new { id = id });
            return Ok(User);
        }

        //[HttpPost]
        //public async Task<ActionResult<UserDTO>> Post(User user)
        //{
        //    UserDTO newUser = await _userRepository.addUser(user);
        //    return Ok(newUser);
        //}

        [HttpPut]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<UserDTO>> Put(UserDTO user)
        {
            UserDTO newUser = await _userRepository.updateUser(user);
            if (newUser == null)
                return BadRequest("could not be updated");
            return Ok(newUser);
        }

        [HttpDelete]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userRepository.deleteUser(id);
            return Ok("user Deleted");
        }
    }
}
