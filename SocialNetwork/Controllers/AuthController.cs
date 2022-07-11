using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace SocialNetwork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _config = configuration;
        }


        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(UserRegisterDTO requestUser)
        {
            User user = new User();

            user.Email = requestUser.Email;
            user.FullName = requestUser.FullName;
            user.Avatar = requestUser.Avatar;
            user.Work = requestUser.Work;
            user.BirthDate = requestUser.BirthDate;
            user.Description = requestUser.Description;
            
            CreatePasswordHash(requestUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            UserDTO newUser = await _userRepository.addUser(user);

            return Ok(newUser);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginDTO requestUser)
        {
            if (await _userRepository.isEmailExisting(requestUser.Email))
            {
                User user = await _userRepository.getUserByEmail(requestUser.Email);
                if (!VerifyPasswordHash(requestUser.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Wrong Password");
                }

                string token = CreateToken(user);
                return Ok(token);
            }
            else
            {
                return NotFound("User Not Found");
            }
        }

        [HttpGet]
        public async Task<ActionResult<UserDTO>> GetMe()
        {
            if (!User.Identity.IsAuthenticated)
                return BadRequest("You are not authenticated");

            Guid userID = new Guid(User.FindFirstValue("id"));
            UserDTO user = await _userRepository.getUserById(userID);

            return Ok(user);
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            Response.Cookies.Delete("jwt");

            return Ok("Success");
        }


        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim("id" , user.Id.ToString())
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                issuer: _config.GetSection("AppSettings:Issuer").Value,
                audience: _config.GetSection("AppSettings:Audience").Value,
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                notBefore: DateTime.Now,
                signingCredentials: cred
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure=true,
            });;

            return jwt;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password ,byte[] passwordHash, byte[] passwordSalt)
        {
            using(var hmac = new HMACSHA512(passwordSalt))
            {
                var ComputedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return ComputedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
