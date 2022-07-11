namespace SocialNetwork.DTOs
{
    public class PostDTO
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = DateTime.Now.ToString("MM/dd/yyyy");
        public string Content { get; set; } = string.Empty;
        public string AuthorId { get; set; } = string.Empty ;
        public string AuthorName { get; set; } = String.Empty;
        public string AuthorAvatar { get; set; } = String.Empty;
        public bool IsLikedByUser { get; set; } = false;
    }

    public class AddPostDto
    {
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public Guid AuthorId { get; set; }
    }
}
