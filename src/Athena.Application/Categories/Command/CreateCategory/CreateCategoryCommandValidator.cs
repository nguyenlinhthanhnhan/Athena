using Athena.DataAccess.Persistence;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Athena.Application.Categories.Command.CreateCategory;

public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
{
    private readonly AthenaDbContext _context;

    public CreateCategoryCommandValidator(AthenaDbContext context)
    {
        _context = context;

        RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required").MaximumLength(200)
            .WithMessage("Title must not exceed 90 characters.").MustAsync(BeUniqueTitle)
            .WithMessage("The specified title already exists.");

        RuleFor(x => x.MetaTitle).NotEmpty().WithMessage("MetaTitle is required").MaximumLength(200);

        RuleFor(x => x.Slug).NotEmpty().WithMessage("Slug is required").MaximumLength(200).MustAsync(BeUniqueSlug)
            .WithMessage("The specified slug already exists.");
    }

    private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AllAsync(l => l.Title != title, cancellationToken: cancellationToken);
    }

    private async Task<bool> BeUniqueSlug(string slug, CancellationToken cancellationToken)
    {
        return await _context.Categories
            .AllAsync(l => l.Slug != slug, cancellationToken: cancellationToken);
    }
}