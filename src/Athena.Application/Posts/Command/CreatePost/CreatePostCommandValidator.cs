using Athena.DataAccess.Persistence;
using FluentValidation;

namespace Athena.Application.Posts.Command.CreatePost;

public class CreatePostCommandValidator : AbstractValidator<CreatePostCommand>
{
    private readonly AthenaDbContext _context;
    
    public CreatePostCommandValidator(AthenaDbContext context)
    {
        _context = context;
        
        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MaximumLength(200)
            .WithMessage("Title must not exceed 90 characters.");

        RuleFor(x => x.MetaTitle).NotEmpty().WithMessage("MetaTitle is required").MaximumLength(200);

        RuleFor(x => x.Slug).NotEmpty().WithMessage("Slug is required").MaximumLength(200);

        RuleFor(x => x.Content).NotEmpty().WithMessage("Content is required");
    }
}