using AutoMapper;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context , IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<UserDTO> addUser(User user)
        {
            using(var context = _context)
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
            }

            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            return await Task.FromResult(userDTO);
        }

        public async Task deleteUser(Guid id)
        {
            using(var context = _context)
            {
                User? user = await context.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (user == null)
                    return;

                context.Entry(user).State = EntityState.Deleted;
                await context.SaveChangesAsync();
            }
        }

        public async Task<UserDTO> getUserById(Guid id)
        {
            using(var context = _context)
            {
                UserDTO? user = await context.Users
                                          .Select(u => _mapper.Map<UserDTO>(u))
                                          .FirstOrDefaultAsync(u => u.Id == id);
                return await Task.FromResult(user);
            }
        }

        public async Task<User> getUserByEmail(string email)
        {
            using(var context = _context)
            {
                User? user = await context.Users.FirstOrDefaultAsync(u => u.Email.Equals(email));
                return await Task.FromResult(user);
            }
        }

        public async Task<List<UserDTO>> getUsers()
        {
            using(var context = _context)
            {
                List<UserDTO> users = await context.Users
                                                .Select(u => _mapper.Map<UserDTO>(u))
                                                .ToListAsync();
                return await Task.FromResult(users);
            }
        }

        public async Task<UserDTO> updateUser(UserDTO user)
        {
            using(var context = _context)
            {
                User? oldUser = await context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);
                if (oldUser != null)
                {
                    oldUser.FullName = user.FullName;
                    oldUser.Avatar = user.Avatar;
                    oldUser.BirthDate = user.BirthDate;
                    oldUser.Description = user.Description;
                    oldUser.Work = user.Work;
                    
                    await context.SaveChangesAsync();

                    UserDTO userDTO = _mapper.Map<UserDTO>(oldUser);

                    return await Task.FromResult(userDTO);
                }

                return null;
            }
        }

        public async Task<bool> isEmailExisting(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user != null)
                return true;
            else 
                return false;
        }
    }
}
