using Athena.DataAccess.Persistence;
using FluentValidation;

namespace Athena.Application.Categories.Command.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly AthenaDbContext _context;

    public CreateCategoryCommandValidator(AthenaDbContext context)
    {
        _context = context;

        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MaximumLength(200)
            .WithMessage("Title must not exceed 90 characters.");

        RuleFor(x => x.MetaTitle).NotEmpty().WithMessage("MetaTitle is required").MaximumLength(200);

        RuleFor(x => x.Slug).NotEmpty().WithMessage("Slug is required").MaximumLength(200);
    }
}