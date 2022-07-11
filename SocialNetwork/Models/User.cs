using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    public class User
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public string FullName { get; set; } = String.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = String.Empty;

        public byte[] PasswordHash { get; set; }

        public byte[] PasswordSalt { get; set; }

        public string Avatar { get;set; } = String.Empty;

        public string Work { get; set; } = String.Empty;

        public string Description { get;set;} = String.Empty; 
        
        [Required]
        public DateTime BirthDate { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
