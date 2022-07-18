using FluentValidation;
using SocialNetwork.Models;

namespace SocialNetwork.Helpers.Validators
{
    public class PostValidator : AbstractValidator<Post>
    {
        public PostValidator()
        {
            RuleFor(post => post.Title).NotEmpty();
            RuleFor(post => post.Description).NotEmpty();
            RuleFor(post => post.Content).NotEmpty();
        }

    }
}
