using SocialNetwork.Models;

namespace SocialNetwork
{
    public static class DummyData
    {
        private static List<User> Users { get; set; } = new List<User>()
        {
               new User()
               {
                   Id = new Guid("11111111-1111-1111-1111-111111111111"),
                   FullName = "Eladraoui chouaib",
                   Email = "eladraoui.chouaib@gmail.com",
                   Avatar = "https://images.unsplash.com/photo-1570295999919-56ceb5ecca61?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80",
                   Work = "FullStack Engineer",
                   Description = "lorem impsum dolor sit amet consectetur adipisicing elit. Quisquam, quidem.",
                   BirthDate = new DateTime(1999,12,06)
               },
               new User()
               {
                   Id = new Guid("11111111-1111-1111-2222-111111111111"),
                   FullName = "Jhon Snow",
                   Email = "Jhon.Snow@gmail.com",
                   Avatar = "https://images.unsplash.com/photo-1619451684167-8099a2636378?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=627&q=80",
                   Work = "Actor",
                   Description = "lorem impsum dolor sit amet consectetur adipisicing elit. Quisquam, quidem.",
                   BirthDate = new DateTime(1999,12,06)
               },
               new User()
               {
                   Id = new Guid("11111111-1111-1111-3333-111111111111"),
                   FullName = "Ali Baba",
                   Email = "Ali.Baba@gmail.com",
                   Avatar = "https://images.unsplash.com/photo-1535713875002-d1d0cf377fde?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80",
                   Work = "Company Owner",
                   Description = "lorem impsum dolor sit amet consectetur adipisicing elit. Quisquam, quidem.",
                   BirthDate = new DateTime(1999,12,06)
               },
               new User()
               {
                   Id = new Guid("11111111-1111-1111-3333-111111111111"),
                   FullName = "tomas tom",
                   Email = "tomas.tom@gmail.com",
                   Avatar = "https://images.unsplash.com/photo-1593726891090-b4c6bc09c819?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80",
                   Work = "model",
                   Description = "lorem impsum dolor sit amet consectetur adipisicing elit. Quisquam, quidem.",
                   BirthDate = new DateTime(1999,12,06)
               }
        };

        private static List<Post> Posts { get; set; } = new List<Post>()
        {
                new Post()
                {
                    Id = new Guid("11111111-1111-1111-0000-111111111111"),
                    Title = "hello",
                    Description = "Lorem ipsum dolor set amet",
                    Content = "https://images.unsplash.com/photo-1655783275139-e7905ee64fa7?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=688&q=80",
                    AuthorId = new Guid("11111111-1111-1111-1111-111111111111")
                },
                new Post()
                {
                    Id = new Guid("11111111-1111-2222-0000-111111111111"),
                    Title = "hello",
                    Description = "Lorem ipsum dolor set amet",
                    Content = "https://images.unsplash.com/photo-1655783275139-e7905ee64fa7?ixlib=rb-1.2.1&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=688&q=80",
                    AuthorId = new Guid("11111111-1111-1111-3333-111111111111")
                }
        };

        public static List<User> getUsers()
        {
            return Users;
        }

        public static List<Post> GetPosts()
        {
            return Posts;
        }

        public static User getUserById(Guid id)
        {
            return Users.Find(x => x.Id == id);
        }

        public static Post getPostById(Guid id)
        {
            return Posts.Find(x => x.Id == id);
        }

        public static List<Post> addPost(Post post)
        {
            var newPosts = Posts;
            Post newPost = new Post()
            {
                Id = post.Id,
                Title = post.Title,
                Description = post.Description,
                Content = post.Content,
                AuthorId = post.AuthorId,
            };
            newPosts.Add(newPost);
            Posts = newPosts;
            return Posts;
        }

        public static List<User> addUser(User user)
        {
            var newUsers = Users;
            User newUser = new User()
            {
                Id = user.Id,
                FullName = user.FullName,
                Avatar = user.Avatar,
                BirthDate = user.BirthDate,
                CreatedDate = user.CreatedDate,
                Description = user.Description,
                Email = user.Email,
                Work = user.Work,
            };
            newUsers.Add(newUser);
            Users = newUsers;
            return Users;
        }

    }
}
