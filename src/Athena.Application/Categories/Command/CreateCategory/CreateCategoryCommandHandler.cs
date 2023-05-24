using Athena.Core.Entities;
using Athena.DataAccess.Persistence;
using AutoMapper;
using MediatR;

namespace Athena.Application.Categories.Command.CreateCategory;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, int>
{
    private readonly AthenaDbContext _context;

    public CreateCategoryCommandHandler(AthenaDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        var newCategory = new Category()
        {
            Title = request.Title,
            MetaTitle = request.MetaTitle,
            Slug = request.Slug,
            Content = request.Content
        };

        _context.Categories.Add(newCategory);
        await _context.SaveChangesAsync(cancellationToken);

        return newCategory.Id;
    }
}