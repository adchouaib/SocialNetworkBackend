using AutoMapper;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Models;

namespace SocialNetwork.Data.Repositories
{
    public class FriendRepository : IFriendRepository
    {

        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public FriendRepository(DataContext context , IMapper mapper )
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task Accept(Guid senderId, Guid receiverId)
        {
            using (var context = _context)
            {
                var friend = context.Friends
                                        .Include(f => f.Status)
                                        .Where(f => f.User1Id == receiverId && f.User2Id == senderId)
                                        .First();

                if (friend != null)
                {
                    friend.Status.StatusName = Status.ACCEPTED;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task<List<PostDTO>> GetFriendsPosts(Guid userId)
        {
            using(var context = _context)
            {
                List<Guid> userFriendsPart1 = context.Friends
                                                     .Include(f => f.Status)
                                                     .Where(p => p.User1Id == userId && p.Status.StatusName == Status.ACCEPTED)
                                                     .Select(p => p.User2Id)
                                                     .ToList();

                List<Guid> userFriendsPart2 = context.Friends
                                                     .Include(f => f.Status)
                                                     .Where(p => p.User2Id == userId && p.Status.StatusName == Status.ACCEPTED)
                                                     .Select(p => p.User1Id)
                                                     .ToList();

                List<Guid> userFriends = userFriendsPart1.Concat(userFriendsPart2).ToList();

                List<PostDTO> friendsPosts = await context.Posts.Include(p => p.User)
                                                          .Where(p => userFriends.Contains(p.User.Id) || p.User.Id == userId)
                                                          .OrderByDescending(p => p.CreatedDate)
                                                          .Select(p => _mapper.Map<PostDTO>(p))
                                                          .ToListAsync();

                friendsPosts.ForEach(fp => fp.IsLikedByUser = context.Likes.FirstOrDefault(l => l.UserId == userId && l.PostId == fp.Id) != null);

                return await Task.FromResult(friendsPosts);
            }
        }



        public async Task<List<UserDTO>> GetNonUserFriends(Guid userId)
        {
            using (var context = _context)
            {
                List<Guid> UserFriendsPart1 = await context.Friends
                                                    .Include(p => p.User1)
                                                    .Include(p => p.Status)
                                                    .Where(p => (p.User1Id == userId || p.User2Id == userId) && p.Status.StatusName == Status.ACCEPTED)
                                                    .Select(p => p.User1Id)
                                                    .ToListAsync();

                List<Guid> UserFriendsPart2 = await context.Friends
                                                    .Include(p => p.User2)
                                                    .Include(p => p.Status)
                                                    .Where(p => (p.User1Id == userId || p.User2Id == userId) && p.Status.StatusName == Status.ACCEPTED)
                                                    .Select(p => p.User2Id)
                                                    .ToListAsync();

                List<Guid> UserFriends = UserFriendsPart1.Concat(UserFriendsPart2).ToList();

                List<UserDTO> NonUserFriends = await context.Users
                                                            .Where(u => !UserFriends.Contains(u.Id))
                                                            .Select(u => _mapper.Map<UserDTO>(u))
                                                            .ToListAsync(); 

                return await Task.FromResult(NonUserFriends);
            }
        }

        public async Task<List<UserDTO>> GetUserFriends(Guid userId)
        {
            using(var context = _context)
            {
                List<UserDTO> UserFriendsPart1 = await context.Friends
                                                    .Include(p => p.User1)
                                                    .Include(p => p.Status)
                                                    .Where(p => (p.User1Id == userId || p.User2Id == userId) && p.Status.StatusName == Status.ACCEPTED )
                                                    .Select(p => _mapper.Map<UserDTO>(p.User1))
                                                    .ToListAsync();

                List<UserDTO> UserFriendsPart2 = await context.Friends
                                                    .Include(p => p.User2)
                                                    .Include(p => p.Status)
                                                    .Where(p => (p.User1Id == userId || p.User2Id == userId) && p.Status.StatusName == Status.ACCEPTED)
                                                    .Select(p => _mapper.Map<UserDTO>(p.User2))
                                                    .ToListAsync();

                List<UserDTO> UserFriends = UserFriendsPart1.Concat(UserFriendsPart2).ToList();
                return await Task.FromResult(UserFriends);
            }
        }

        public async Task<List<UserDTO>> GetUserInvitations(Guid userId)
        {
            using(var context = _context)
            {
                List<UserDTO> invitations = context.Friends
                                                   .Include(f => f.Status)
                                                   .Include(f => f.User1)
                                                   .Where(f => f.User2Id == userId && f.Status.StatusName == Status.PENDING)
                                                   .Select(f => _mapper.Map<UserDTO>(f.User2))
                                                   .ToList();

                return await Task.FromResult(invitations);
            }
        }

        public async Task Invite(Guid senderId, Guid receiverId)
        {
            using(var context = _context)
            {
                var friend = context.Friends
                                    .Include(f => f.Status)
                                    .FirstOrDefault(f => (f.User1Id == senderId && f.User2Id == receiverId)
                                                      || (f.User1Id == receiverId && f.User2Id == senderId)
                                                    );
                if (friend != null)
                {
                    if(friend.User1Id == receiverId && friend.User2Id == senderId)
                    {
                        friend.User1Id = Guid.Empty;
                        friend.User2Id = Guid.Empty;
                        friend.User1 = context.Users.Where(f => f.Id == senderId).First();
                        friend.User2 = context.Users.Where(f => f.Id == receiverId).First();
                    }
                    friend.Status.StatusName = Status.PENDING;
                    await context.SaveChangesAsync();
                }
                else
                {
                    User user1 = context.Users.Where(u => u.Id == senderId).First();
                    User user2 = context.Users.Where(u => u.Id == receiverId).First();

                    Friends f = new Friends()
                    {
                        User1 = user1,
                        User2 = user2,
                    };

                    FriendShipStatus status = new FriendShipStatus()
                    {
                        Friends = f,
                        StatusName = Status.PENDING
                    };

                    context.FriendShipStatuses.Add(status);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task Refuse(Guid senderId, Guid receiverId)
        {
            using (var context = _context)
            {
                var friend = context.Friends
                                        .Include(f => f.Status)
                                        .Where(f => f.User1Id == receiverId && f.User2Id == senderId)
                                        .First();

                if (friend != null)
                {
                    friend.Status.StatusName = Status.REJECTED;
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task Unfriend(Guid senderId, Guid receiverId)
        {
            using (var context = _context)
            {
                Friends friend = context.Friends.First(f => (f.User1Id == senderId && f.User2Id == receiverId)
                                                             || (f.User1Id == receiverId && f.User2Id == senderId)
                                                           );
    
                if(friend != null)
                {
                    context.Entry(friend).State = EntityState.Deleted;
                }
            }
        }
    }
}
