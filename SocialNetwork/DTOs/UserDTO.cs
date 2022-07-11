namespace SocialNetwork.DTOs
{
    public class UserDTO
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = String.Empty;
        public string Avatar { get; set; } = String.Empty;
        public string Work { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
    }

    public class UserRegisterDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string FullName { get; set; } = String.Empty;
        public string Avatar { get; set; } = String.Empty;
        public string Work { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;
        public DateTime BirthDate { get; set; }
    }

    public class UserLoginDTO
    {
        public string Email { get; set; }= string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
