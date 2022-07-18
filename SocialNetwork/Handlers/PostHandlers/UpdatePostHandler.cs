using AutoMapper;
using FluentValidation.Results;
using MediatR;
using SocialNetwork.Commands;
using SocialNetwork.Data.IRepositories;
using SocialNetwork.DTOs;
using SocialNetwork.Helpers.Validators;
using SocialNetwork.Models;

namespace SocialNetwork.Handlers.PostHandlers
{
    public class UpdatePostHandler : IRequestHandler<UpdatePostCommand, PostDTO>
    {
        private readonly IPostRepository _postRepository;
        private readonly IMapper _mapper;
        private PostValidator postValidator;

        public UpdatePostHandler(IPostRepository postRepository, IMapper mapper)
        {
            _postRepository = postRepository;
            _mapper = mapper;
            postValidator = new PostValidator();
        }

        public async Task<PostDTO> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            PostDTO post = new PostDTO()
            {
                Title = request.Title,
                Description = request.Description,
                CreatedDate = DateTime.Now,
                AuthorId = Guid.Empty.ToString(),
                Content = request.Content,
            };
            Post newPost = _mapper.Map<Post>(post);

            ValidationResult result = postValidator.Validate(newPost);
            if (result.IsValid)
            {
                var Post = await _postRepository.updatePost(post, request.PostId);
                return Post;
            }
            else
            {
                var error = "";
                foreach (var failure in result.Errors)
                {
                    error = "Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage;
                }
                throw new Exception(error);
            }
        }
    }
}
