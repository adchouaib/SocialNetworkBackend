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
    public class AddPostHandler : IRequestHandler<AddPostCommand, PostDTO>
    {
        private readonly IMapper _mapper;
        private readonly IPostRepository _postRepository;
        private PostValidator postValidator;

        public AddPostHandler(IPostRepository postRepository, IMapper mapper)
        {
            _mapper = mapper;
            _postRepository = postRepository;
            postValidator = new PostValidator();
        }

        public async Task<PostDTO> Handle(AddPostCommand request, CancellationToken cancellationToken)
        {
            PostDTO post = new PostDTO()
            {
                Title = request.Title,
                Description = request.Description,
                Content = request.Content,
                CreatedDate = DateTime.Now,
                AuthorId = request.AuthorId.ToString()
            };
            Post newPost = _mapper.Map<Post>(post);
            ValidationResult result = postValidator.Validate(newPost);

            if (result.IsValid)
            {
                return await _postRepository.addPost(newPost);
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
